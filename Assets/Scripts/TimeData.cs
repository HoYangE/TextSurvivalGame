using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeData : MonoBehaviour
{
    [SerializeField] private TMP_Text yearText;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text timeText;

    public static TimeData Instance { get; private set; }

    public float TimeScale { get; private set; }

    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }

    private int[] _daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private void InitTime()
    {
        SetDate(1001, 1, 1);
        SetTime(0, 0);
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
        Hour = hour;
        Minute = minute;
    }

    public void AddTime(int hour, int minute)
    {
        Hour += hour;
        Minute += minute;
        timeText.text = $"{Hour:D2}:{Minute:D2}";
        HourUpdate();
        DayUpdate();
    }

    private void HourUpdate()
    {
        if (Minute < 60) return;
        Hour++;
        Minute -= 60;
        timeText.text = $"{Hour:D2}:{Minute:D2}";
    }

    private void DayUpdate()
    {
        if (Hour < 24) return;
        Hour -= 24;
        timeText.text = $"{Hour:D2}:{Minute:D2}";
        AddDate(0, 0, 1);
    }

    public void SetDate(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;

        yearText.text = $"{year:D4}";
        dateText.text = $"{month:D2}.{day:D2}";
    }

    public void AddDate(int year, int month, int day)
    {
        Year += year;
        Month += month;
        Day += day;
        
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
        Day = currentDay + addDay - GetMaxDaysInMonth(month, year);
        Month = monthTextValue;
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
        
        Year = yearTextValue + 1;
        Month = 1;
        Day = dayTextValue;
    }

}
