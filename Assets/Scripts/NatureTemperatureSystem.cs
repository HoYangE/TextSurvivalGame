using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureTemperatureSystem : MonoBehaviour
{
    private int _day;
    private int _today;
    private float _natureDayTemperature;
    private float _natureTimeTemperature;
    private const int MinutePerDay = 1440;

    // 1월 27일
    private int _coldestDay = 27;
    // 7월 29일
    private int _hottestDay = 210;
    private float _highestDayTemperature = 30;
    private float _lowestDayTemperature = -5;
    // 7시
    private int _coldestTime = 420;
    // 15시
    private int _hottestTime = 900;
    private float _highestTimeTemperature = 5;
    private float _lowestTimeTemperature = -5;

    private TimeData _timeData;
    
    public static NatureTemperatureSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeData = TimeData.Instance;
        
        _day = _timeData.IsLeapYear(_timeData.Year) ? 366 : 365;
        _hottestDay = _timeData.IsLeapYear(_timeData.Year) ? 211 : 210;

        _today = CalcToday();
    }

    public float GetNatureTemperature()
    {
        return CalcNatureDayTemperature(_today) + CalcNatureTimeTemperature(_timeData.Hour * 60 + _timeData.Minute);
    }
    
    private int CalcToday()
    {
        return new DateTime(_timeData.Year, _timeData.Month, _timeData.Day).DayOfYear;
    }

    private float CalcNatureDayTemperature(int day)
    {
        if (day >= _coldestDay && day <= _hottestDay)
            return Mathf.Lerp(_lowestDayTemperature, _highestDayTemperature,
                (float)(day - _coldestDay) / (_hottestDay - _coldestDay));
        return day < _coldestDay
            ? Mathf.Lerp(_highestDayTemperature, _lowestDayTemperature,
                (float)(day + _day - _hottestDay) / (_coldestDay + _day - _hottestDay))
            : Mathf.Lerp(_highestDayTemperature, _lowestDayTemperature,
                (float)(day - _hottestDay) / (_coldestDay + _day - _hottestDay));
    }

    private float CalcNatureTimeTemperature(int minute)
    {
        if (minute >= _coldestTime && minute <= _hottestTime)
            return Mathf.Lerp(_lowestTimeTemperature, _highestTimeTemperature,
                (float)(minute - _coldestTime) / (_hottestTime - _coldestTime));
        return minute < _coldestDay
            ? Mathf.Lerp(_highestTimeTemperature, _lowestTimeTemperature,
                (float)(minute + MinutePerDay - _hottestTime) / (_coldestTime + MinutePerDay - _hottestTime))
            : Mathf.Lerp(_highestTimeTemperature, _lowestTimeTemperature,
                (float)(minute - _hottestTime) / (_coldestTime + MinutePerDay - _hottestTime));
    }
}
