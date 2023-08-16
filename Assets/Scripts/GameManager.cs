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
        //StartCoroutine(EndTimer());
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

    IEnumerator EndTimer()
    {
        var elapsed = 0.0f;
        const int timeLength = 144;
        
        while (elapsed <= timeLength)
        {
            yield return null;
            elapsed += Time.deltaTime;
        }
        
        Debug.Log("게임 종료");
        EditorApplication.isPaused = true;
    }
}
