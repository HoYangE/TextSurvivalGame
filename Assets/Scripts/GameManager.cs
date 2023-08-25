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
        TimeManager.Instance.SetTimeScale(timeScale);
        DataCheck();
        StartCoroutine(TimeCoroutine());
    }

    private void DataCheck()
    {
        DataManager.Instance.CheckStatusData();
        DataManager.Instance.CheckTimeData();
        DataManager.Instance.CheckPlayerData();
        DataManager.Instance.CheckMovePopUpData();
    }
    
    public void GameOver()
    {
        
    }

    public void GameOut()
    {
        DataManager.Instance.SaveStatusData();
        DataManager.Instance.SaveTimeData();
        DataManager.Instance.SavePlayerData();
        DataManager.Instance.SaveMovePopUpData();
    }
    
    IEnumerator TimeCoroutine()
    {
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
            TimeManager.Instance.AddTime(0,1);
        }
    }
}
