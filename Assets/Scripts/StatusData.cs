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

    public void AddTemperature(float value)
    {
        Temperature += value;
        if (Temperature <= 28 && Temperature >= 42) GameManager.Instance.GameOver();
    }

    public void InitStatus()
    {
        Hunger = 100;
        Moisture = 100;
        Stress = 0;
        Temperature = 36.5f;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(HungerCoroutine());
        StartCoroutine(MoistureCoroutine());
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
}
