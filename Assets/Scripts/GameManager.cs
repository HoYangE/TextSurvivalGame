using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeScale;
    
    public Vector2 PlayerPosition { get; set; }
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 144;
    }

    private void Start()
    {
        TimeData.Instance.SetTimeScale(timeScale);
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
