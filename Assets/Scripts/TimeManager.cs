using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TMP_Text yearText;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text timeText;

    public static TimeManager Instance { get; private set; }

    public float TimeScale { get; private set; }

    public TimeData TimeData { get; set; } = new TimeData();
    


    private int[] _daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private void InitTime()
    {
        SetDate(1001, 4, 1);
        SetTime(10, 0);
    }

    private void Awake()
    {
        Instance = this;
        TimeScale = 1;
        InitTime();
    }

    public void SetTimeScale(float timeScale)
    {
        TimeScale = timeScale;
    }

    public void SetTime(int hour, int minute)
    {
        TimeData.hour = hour;
        TimeData.minute = minute;
    }

    public void AddTime(int hour, int minute)
    {
        TimeData.hour += hour;
        TimeData.minute += minute;
        timeText.text = $"{TimeData.hour:D2}:{TimeData.minute:D2}";
        HourUpdate();
        DayUpdate();
    }

    private void HourUpdate()
    {
        if (TimeData.minute < 60) return;
        TimeData.hour++;
        TimeData.minute -= 60;
        timeText.text = $"{TimeData.hour:D2}:{TimeData.minute:D2}";
    }

    private void DayUpdate()
    {
        if (TimeData.hour < 24) return;
        TimeData.hour -= 24;
        timeText.text = $"{TimeData.hour:D2}:{TimeData.minute:D2}";
        AddDate(0, 0, 1);
    }

    public void SetDate(int year, int month, int day)
    {
        TimeData.year = year;
        TimeData.month = month;
        TimeData.day = day;

        yearText.text = $"{year:D4}";
        dateText.text = $"{month:D2}.{day:D2}";
    }

    public void AddDate(int year, int month, int day)
    {
        TimeData.year += year;
        TimeData.month += month;
        TimeData.day += day;
        
        var yearTextValue = int.Parse(yearText.text);
        var dateTextValue = dateText.text.Split('.');
        var monthTextValue = int.Parse(dateTextValue[0]);
        var dayTextValue = int.Parse(dateTextValue[1]);

        yearText.text = $"{yearTextValue + year:D4}";
        dateText.text = $"{monthTextValue + month:D2}.{dayTextValue + day:D2}";

        MonthUpdate(dayTextValue, day, monthTextValue, yearTextValue, dateText, monthTextValue);
        YearUpdate();
    }

    private void MonthUpdate(int currentDay, int addDay, int month, int year, TMP_Text dateTextComponent, int monthTextValue)
    {
        if (currentDay + addDay <= GetMaxDaysInMonth(month, year)) return;
        monthTextValue++;
        dateTextComponent.text = $"{monthTextValue:D2}.{currentDay + addDay - GetMaxDaysInMonth(month, year):D2}";
        TimeData.day = currentDay + addDay - GetMaxDaysInMonth(month, year);
        TimeData.month = monthTextValue;
    }

    private int GetMaxDaysInMonth(int month, int year)
    {
        if (IsLeapYear(year))
            _daysInMonth[1] = 29;
        else
            _daysInMonth[1] = 28;

        return _daysInMonth[month - 1];
    }

    public bool IsLeapYear(int year)
    {
        return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
    }

    private void YearUpdate()
    {
        var yearTextValue = int.Parse(yearText.text);
        var dateTextValue = dateText.text.Split('.');
        var monthTextValue = int.Parse(dateTextValue[0]);
        var dayTextValue = int.Parse(dateTextValue[1]);

        if (monthTextValue != 13) return;
        yearText.text = $"{yearTextValue + 1:D4}";
        dateText.text = $"01.{dayTextValue:D2}";
        
        TimeData.year = yearTextValue + 1;
        TimeData.month = 1;
        TimeData.day = dayTextValue;
    }

}
