using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        TimeData.Instance.SetDate(1001,1,1);
        TimeData.Instance.SetTime(0,0);
        TimeData.Instance.SetTimeScale(0.1f);
        StartCoroutine(TimeCoroutine());
    }

    void Update()
    {
        
    }

    IEnumerator TimeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeData.Instance.TimeScale);
            TimeData.Instance.AddTime(0,1);
        }
    }
}
