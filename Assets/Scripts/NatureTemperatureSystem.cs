using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureTemperatureSystem : MonoBehaviour
{
    // 달에 따라 기본 온도 설정
    // 시간에 따라 기본 온도에서 약간의 변화
    
    // 가장 추운달 1월, 가장 더운달 8월
    // 1월27일 -8도, 7월29일 25도 -> 27일 -8도 210일 25도
    // 시간에 따라 +- 10도
    // 7시 -10도 15시 +10도

    private int _day;
    private int _today;
    private float _natureTemperature;

    private int _coldestDay = 27;
    private int _hottestDay = 210;
    private float _highestTemperature = 25;
    private float _lowestTemperature = -8;
    
    private TimeData _timeData;
    
    private void Start()
    {
        _timeData = TimeData.Instance;
        
        _day = _timeData.IsLeapYear(_timeData.Year) ? 366 : 365;
        _hottestDay = _timeData.IsLeapYear(_timeData.Year) ? 211 : 210;

        _today = CalcToday();
        
        for(int i=1;i<=365;i++)
            Debug.Log($"{i} : {CalcNatureTemperature(i)}");

    }

    private int CalcToday()
    {
        return new DateTime(_timeData.Year, _timeData.Month, _timeData.Day).DayOfYear;
    }

    private float CalcNatureTemperature(int day)
    {
        if(day >= _coldestDay && day <= _hottestDay) return Mathf.Lerp(_lowestTemperature, _highestTemperature, (float) (day - _coldestDay) / (_hottestDay - _coldestDay));
        return day < _coldestDay ? Mathf.Lerp(25, -8, (float) (day+_day - _hottestDay) / (_coldestDay+_day - _hottestDay)) : Mathf.Lerp(25, -8, (float) (day - _hottestDay) / (_coldestDay+_day - _hottestDay));
    }
}
