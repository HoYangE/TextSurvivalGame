using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeData : MonoBehaviour
{
    [SerializeField]
    private GameObject yearText;
    [SerializeField]
    private GameObject dateText;
    [SerializeField]
    private GameObject timeText;
    
    public static TimeData Instance { get; private set; }

    public float TimeScale { get; private set; }
    public int Hour { get; private set; }
    public int Minute { get; private set; }

    private int[] daysInMonth = {31,28,31,30,31,30,31,31,30,31,30,31};
    
    private void Awake()
    {
        Instance = this;
        TimeScale = 1;
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
        timeText.GetComponent<TMP_Text>().text = $"{Hour:D2}:{Minute:D2}";
        HourUpdate();
        DayUpdate();
    }

    private void HourUpdate()
    {
        if (Minute < 60) return;
        Hour++;
        Minute -= 60;
        timeText.GetComponent<TMP_Text>().text = $"{Hour:D2}:{Minute:D2}";
    }

    private void DayUpdate()
    {
        if (Hour < 24) return;
        Hour -= 24;
        timeText.GetComponent<TMP_Text>().text = $"{Hour:D2}:{Minute:D2}";
        AddDate(0, 0, 1);
    }
    
    public void SetDate(int year, int month, int day)
    {
        yearText.GetComponent<TMP_Text>().text = $"{year:D4}";
        dateText.GetComponent<TMP_Text>().text = $"{month:D2}.{day:D2}";
    }
    
    public void AddDate(int year, int month, int day)
    {
        var yearTextComponent = yearText.GetComponent<TMP_Text>();
        var dateTextComponent = dateText.GetComponent<TMP_Text>();
        var yearTextValue = int.Parse(yearTextComponent.text);
        var dateTextValue = dateTextComponent.text.Split('.');
        var monthTextValue = int.Parse(dateTextValue[0]);
        var dayTextValue = int.Parse(dateTextValue[1]);
        
        yearTextComponent.text = $"{yearTextValue + year:D4}";
        dateTextComponent.text = $"{monthTextValue + month:D2}.{dayTextValue + day:D2}";

        MonthUpdate(dayTextValue, day, monthTextValue, yearTextValue, dateTextComponent, monthTextValue);
        YearUpdate();
    }
    
    private void MonthUpdate(int currentDay, int addDay,int month,int year, TMP_Text dateTextComponent, int monthTextValue)
    {
        if (currentDay + addDay <= GetMaxDaysInMonth(month, year)) return;
        monthTextValue++;
        dateTextComponent.text = $"{monthTextValue:D2}.{currentDay + addDay - GetMaxDaysInMonth(month,year):D2}";
    }
    
    private int GetMaxDaysInMonth(int month,int year)
    {
        if(IsLeapYear(year))
            daysInMonth[1] = 29;
        else
            daysInMonth[1] = 28;

        return daysInMonth[month - 1];
    }
    
    private static bool IsLeapYear(int year)
    {
        return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
    }
    
    private void YearUpdate()
    {
        var yearTextComponent = yearText.GetComponent<TMP_Text>();
        var dateTextComponent = dateText.GetComponent<TMP_Text>();
        var yearTextValue = int.Parse(yearTextComponent.text);
        var dateTextValue = dateTextComponent.text.Split('.');
        var monthTextValue = int.Parse(dateTextValue[0]);
        var dayTextValue = int.Parse(dateTextValue[1]);
        
        if (monthTextValue != 13) return;
        yearTextComponent.text = $"{yearTextValue + 1:D4}";
        dateTextComponent.text = $"01.{dayTextValue:D2}";
    }
}
