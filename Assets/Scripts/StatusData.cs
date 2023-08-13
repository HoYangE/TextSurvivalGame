using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusData : MonoBehaviour
{
    [SerializeField]
    private GameObject hungerText;
    [SerializeField]
    private GameObject moistureText;
    [SerializeField]
    private GameObject stressText;
    [SerializeField]
    private GameObject temperatureText;
    
    public static StatusData Instance { get; private set; }

    private int hunger;
    private int moisture;
    private int stress;
    private int temperature;
    
    private void Awake()
    {
        Instance = this;
    }
    
}
