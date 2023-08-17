using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusData : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hungerText;
    [SerializeField]
    private TMP_Text moistureText;
    [SerializeField]
    private TMP_Text stressText;
    [SerializeField]
    private TMP_Text temperatureText;
    
    public static StatusData Instance { get; private set; }

    private int _hunger;
    private int _moisture;
    private int _stress;
    private float _temperature;

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
            temperatureText.text = value.ToString();
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
        Temperature = value;
        if (Temperature <= 28 && Temperature >= 42) GameManager.Instance.GameOver();
    }

    private void InitStatus()
    {
        Hunger = 100;
        Moisture = 100;
        Stress = 0;
        Temperature = 36.5f;
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
        var timeLength = TimeData.Instance.TimeScale * (3*7*24*60) / 100;
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
        var timeLength = TimeData.Instance.TimeScale * (3*24*60) / 100;
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
        var timeLength = TimeData.Instance.TimeScale;
        while (true)
        {
            while (elapsed <= timeLength)
            {
                yield return null;
                elapsed += Time.deltaTime;
            }
            elapsed -= timeLength;

            /*
             * 체감온도 -12.7~38.08
             * 열원 -20~20
             * 단열비율 0~1
             * 질병 -20~20
             *
             * 28~42 = (-10~35(-12.7~38.08) + -20~20) + (0~1) + (-20~20)
             * 체온 = 주변델타(열원) + 옷(단열 비율) + 질병(추가요소)
             * 주변델타 = 날씨+모닥불 (열원이 없으면 초당 -1도)
             * 단열 비율이 0이면 주변델타와 질병으로만 결정 / 1이면 36.5 고정
             * 비율이 높을 수록 따라 체온이 36.5에 근접하게 나와야함
             */
            float t = NatureTemperatureSystem.Instance.GetNatureTemperature();
            float v = 1.3f;
            var influenceTemperature = 13.12f + 0.6215f * t - 11.37f * Mathf.Pow(v,0.16f) + 0.3965f * Mathf.Pow(v,0.16f) * t;
 
            //SetTemperature(36.5f * influenceTemperature);
        }
    }
}
