using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeScale;
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(TimeCoroutine());
    }

    public void GameOver()
    {
        
    }
    
    IEnumerator TimeCoroutine()
    {
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
            TimeData.Instance.AddTime(0,1);
        }
    }
}
