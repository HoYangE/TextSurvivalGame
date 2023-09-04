using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeScale;
    
    public Vector2 PlayerPosition { get; set; }
    public float MoveNormTime { get; set; }
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 30;
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

    public void StartMoving()
    {
        StartCoroutine(MovingTimeCoroutine());
    }

    private void EndMoving()
    {
        ApplyMoveData();
        UpdatePopUp();
        ResetMovePopUpData();
    }

    private void ApplyMoveData()
    {
        StatusManager.Instance.SetPositionName(DataManager.Instance.MovePopUpData.name);
        PlayerPosition = DataManager.Instance.MovePopUpData.position;
        MoveNormTime = 0;
    }
    
    private void UpdatePopUp()
    {
        GameObject.Find("Choice").TryGetComponent<PopUpButton>(out var popUpButton);
        popUpButton.UpdatePopUp();
        popUpButton.TouchBlock.SetActive(false);
    }
    
    private void ResetMovePopUpData()
    {
        DataManager.Instance.MovePopUpData.name = null;
        DataManager.Instance.MovePopUpData.position = Vector2.zero;
        DataManager.Instance.MovePopUpData.time = 0;
        DataManager.Instance.MovePopUpData.timeLength = 0;
    }
    
    IEnumerator MovingTimeCoroutine()
    {
        var elapsed = Time.realtimeSinceStartup - DataManager.Instance.MovePopUpData.time;
        var timeLength = TimeManager.Instance.TimeScale * (DataManager.Instance.MovePopUpData.timeLength * 60);
        
        while (elapsed <= timeLength)
        {
            yield return null;
            elapsed += Time.deltaTime;
            MoveNormTime = elapsed / timeLength;
        }

        EndMoving();
    }
}
