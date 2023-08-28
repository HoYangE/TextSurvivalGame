using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hungerText;
    [SerializeField]
    private TMP_Text moistureText;
    [SerializeField]
    private TMP_Text stressText;
    [SerializeField]
    private TMP_Text temperatureText;
    [SerializeField]
    private TMP_Text positionText;
    
    public static StatusManager Instance { get; private set; }

    private int _hunger;
    private int _moisture;
    private int _stress;
    private float _temperature;
    private string _positionName;
    
    private float _heatSource;
    private float _insulation;
    private float _disease;
    
    public int Hunger 
    {
        get => _hunger;
        private set
        {
            _hunger = value;
            hungerText.text = value.ToString();
        }
    }

    public int Moisture
    {
        get => _moisture;
        private set
        {
            _moisture = value;
            moistureText.text = value.ToString();
        }
    }

    public int Stress 
    { 
        get => _stress;
        private set
        {
            _stress = value;
            stressText.text = value.ToString();
        }
    }

    public float Temperature
    {
        get => _temperature;
        private set
        {
            _temperature = value;
            temperatureText.text = (Mathf.Floor(value*10f)/10f).ToString("F1");
        }
    }

    public string PositionName
    {
        get => _positionName;
        private set
        {
            _positionName = value;
            positionText.text = value;
        }
    }
    
    public void AddHunger(int value) 
    { 
        Hunger += value;
        if (Hunger < 0)
        {
            Hunger = 0;
            GameManager.Instance.GameOver();
        }
    }

    public void AddMoisture(int value)
    {
        Moisture += value;
        if (Moisture < 0)
        {
            Moisture = 0;
            GameManager.Instance.GameOver();
        }
    }
    
    public void AddStress(int value)
    {
        Stress += value;
        if (Stress >= 100)
        {
            Stress = 100;
            GameManager.Instance.GameOver();
        }
    }
    
    public void SetTemperature(float value)
    {
        // -52.7 ~ 78.08 = (-10~35(-12.7~38.08) + -20~20) + (0~1) + (-20~20)
        // -32.7 ~ 58.08
        Temperature = value;
        if (Temperature <= -10 && Temperature >= 45) GameManager.Instance.GameOver();
    }

    public void SetPositionName(string value)
    {
        PositionName = value;
    }
    
    private void InitStatus()
    {
        Hunger = 100;
        Moisture = 100;
        Stress = 0;
        Temperature = 36.5f;
        PositionName = "베이스캠프";
        _heatSource = 0;
        _insulation = 0.3f;
        _disease = 0;
    }
    
    private void Awake()
    {
        Instance = this;
        InitStatus();
    }

    private void Start()
    {
        StartCoroutine(HungerCoroutine());
        StartCoroutine(MoistureCoroutine());
        StartCoroutine(TemperatureCoroutine());
    }

    IEnumerator HungerCoroutine()
    {
        // 3weeks
        var elapsed = 0.0f;
        var timeLength = TimeManager.Instance.TimeScale * (3*7*24*60) / 100;
        while (true)
        {
            while (elapsed <= timeLength)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }
            elapsed -= timeLength;

            AddHunger(-1);
        }
    }
    
    IEnumerator MoistureCoroutine()
    {
        // 3days
        var elapsed = 0.0f;
        var timeLength = TimeManager.Instance.TimeScale * (3*24*60) / 100;
        while (true)
        {
            while (elapsed <= timeLength)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }
            elapsed -= timeLength;

            AddMoisture(-1);
        }
    }
    
    IEnumerator TemperatureCoroutine()
    {
        // 30minutes
        var elapsed = 0.0f;
        var timeLength = TimeManager.Instance.TimeScale;
        while (true)
        {
            while (elapsed <= timeLength)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }
            elapsed -= timeLength;

            SetTemperature(CalcTemperature());
        }
    }
    
    private float CalcTemperature()
    {
        /*
        * 체감온도 -12.7~38.08
        * 열원 -20~20
        * 단열비율 0~1
        * 질병 -20~20
        * 생존온도 10~50
        * 
        * -52.7 ~ 78.08 = (-10~35(-12.7~38.08) + -20~20) + (0~1) + (-20~20)
        * 
        * 체온 = 주변델타(열원) + 옷(단열 비율) + 질병(추가요소)
        */
        
        var t = NatureTemperatureSystem.Instance.GetNatureTemperature();
            
        const float v = 1.3f;
        var influenceTemperature = 13.12f + 0.6215f * t - 11.37f * Mathf.Pow(v,0.16f) + 0.3965f * Mathf.Pow(v,0.16f) * t;

        var targetTemperature = (influenceTemperature + (_insulation - 0) * ((36.5f - influenceTemperature) / (1 - 0))) + _heatSource + _disease;
        var temperatureDelta = targetTemperature - _temperature;
        var alpha = Mathf.Clamp01(Mathf.Abs(temperatureDelta) / 100);
        
        return Mathf.Lerp(_temperature, targetTemperature, Mathf.Min(0.15f, alpha));
    }
}
