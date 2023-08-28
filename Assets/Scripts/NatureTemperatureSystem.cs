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

    private TimeManager _timeManager;
    
    public static NatureTemperatureSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeManager = TimeManager.Instance;
        
        _day = _timeManager.IsLeapYear(_timeManager.TimeData.year) ? 366 : 365;
        _hottestDay = _timeManager.IsLeapYear(_timeManager.TimeData.year) ? 211 : 210;

        _today = CalcToday();
    }

    public float GetNatureTemperature()
    {
        return CalcNatureDayTemperature(_today) + CalcNatureTimeTemperature(_timeManager.TimeData.hour * 60 + _timeManager.TimeData.minute);
    }
    
    private int CalcToday()
    {
        return new DateTime(_timeManager.TimeData.year, _timeManager.TimeData.month, _timeManager.TimeData.day).DayOfYear;
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
