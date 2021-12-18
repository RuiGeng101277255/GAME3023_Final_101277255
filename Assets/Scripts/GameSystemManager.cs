using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class GameSystemManager : MonoBehaviour
{
    public TMP_Text CurrentTimeText;
    public TMP_Text CalendarText;
    public CalendarManager calendarManager;

    public TMP_Text TimeScaleText;
    private float secPerSec = 1.0f;
    private float minPerSec = 60.0f;
    private float hrPerSec = 3600.0f;
    private float dayPerSec = 86400.0f;
    private float CurrentTimeSpeedFactorBySeconds;
    private float totalRunTime = 0.0f;
    public Light2D GlobalDayLight;
    private float initialDayLightIntensity;

    //Date
    public int currentDay;
    public int maxMonthDay;
    public int currentMonth;
    public string MonthName;
    public int currentYear;
    public bool isLeapYear;
    //Time
    private int currentTime_Hour;
    private int currentTime_Minute;
    private float currentTime_Seconds;

    // Start is called before the first frame update
    void Start()
    {
        currentTime_Hour = 0;
        currentTime_Minute = 0;
        currentTime_Seconds = 0.0f;

        currentDay = 29;
        maxMonthDay = 31;
        currentMonth = 1;
        MonthName = "January";
        currentYear = 2021;
        isLeapYear = false;

        CurrentTimeSpeedFactorBySeconds = 1.0f;

        MonthPopulation(currentYear, currentMonth);
        initialDayLightIntensity = GlobalDayLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeValue();
        updateTimeText();
        updateDayLightColor();
        updateCalendarValue();
        updateCalendarText();
    }

    void MonthPopulation(int year, int month)
    {
        string targetDaysString = CalendarContentSavingScript.Instance().LoadStringContentsByMonthAndYear(year, month);
        if (targetDaysString != "")
        {
            calendarManager.setDaysAndContentsUsingString(year, month, targetDaysString);
        }
        else
        {
            calendarManager.populateEmptyMonth(year, month);
        }

        calendarManager.ShowCurrentDayHighlighted();
    }

    void updateTimeValue()
    {
        currentTime_Seconds += Time.deltaTime * CurrentTimeSpeedFactorBySeconds;

        if (currentTime_Seconds >= 60.0f)
        {
            while (currentTime_Seconds >= 60.0f)
            {
                currentTime_Minute++;
                currentTime_Seconds -= 60.0f;
            }
        }

        if (currentTime_Minute >= 60)
        {
            while (currentTime_Minute >= 60)
            {
                currentTime_Hour++;
                currentTime_Minute -= 60;
            }
        }

        if (currentTime_Hour >= 24)
        {
            while (currentTime_Hour >= 24)
            {
                currentDay++;
                currentTime_Hour -= 24;
                calendarManager.ShowCurrentDayHighlighted();
            }
        }
    }

    void updateTimeText()
    {
        CurrentTimeText.text = MonthName + " " 
            + currentDay + MonthStructureScript.Instance().getDayPostFix(currentDay) + "," 
            + currentYear + " " + currentTime_Hour.ToString("00") + ": " 
            + currentTime_Minute.ToString("00") + ": " 
            + ((int)currentTime_Seconds).ToString("00");
    }

    void updateDayLightColor()
    {
        float runtimeDaySpeedInSeconds = CurrentTimeSpeedFactorBySeconds / 43200.0f; //half a day
        totalRunTime += Time.deltaTime * runtimeDaySpeedInSeconds;
        GlobalDayLight.intensity = Mathf.Lerp(0.0f, initialDayLightIntensity, Mathf.PingPong(totalRunTime, 1.0f)); ;
    }

    void updateCalendarValue()
    {
        checkForLeapYear();
        updateMonthValue();
        updateDayValue();
    }

    void checkForLeapYear()
    {
        if (currentYear % 4 == 0)
        {
            isLeapYear = true;
        }
        else
        {
            isLeapYear = false;
        }
    }

    void updateMonthValue()
    {
        MonthName = MonthStructureScript.Instance().getTargetMonthName(currentMonth);
        maxMonthDay = MonthStructureScript.Instance().getTargetMonthMaxDayNumber(currentYear, currentMonth);
    }

    void updateDayValue()
    {
        if (currentDay > maxMonthDay)
        {
            if (currentMonth != 12)
            {
                currentMonth++;
            }
            else
            {
                currentMonth = 1;
                currentYear++;
            }
            currentDay = 1;
            updateMonthValue();
            MonthPopulation(currentYear, currentMonth);
        }
    }

    void updateCalendarText()
    {
        CalendarText.text = MonthStructureScript.Instance().getTargetMonthName(calendarManager.CalendarDisplayMonth) + " of " + calendarManager.CalendarDisplayYear;
    }

    public void setTime(int hour, int minute, int seconds)
    {
        currentTime_Hour = hour;
        currentTime_Minute = minute;
        currentTime_Seconds = (float)seconds;
    }

    public void setDate(int year, int month, int day)
    {
        currentYear = year;
        currentMonth = month;
        currentDay = day;
    }

    public void monthChangePressed(bool isNext)
    {
        calendarManager.saveCurrentMonth();

        int targetMonth = calendarManager.CalendarDisplayMonth;
        int targetYear = calendarManager.CalendarDisplayYear;

        if (isNext)
        {
            targetMonth++;
        }
        else
        {
            targetMonth--;
        }

        if (targetMonth > 12)
        {
            targetMonth = 1;
            targetYear++;
        }
        else if (targetMonth < 1)
        {
            targetMonth = 12;
            targetYear--;
        }

        //updateMonthValue();

        MonthPopulation(targetYear, targetMonth);
    }

    public void TimeScalePressed()
    {
        if (CurrentTimeSpeedFactorBySeconds == secPerSec)
        {
            CurrentTimeSpeedFactorBySeconds = minPerSec;
            TimeScaleText.text = "1 min/s";
        }
        else if (CurrentTimeSpeedFactorBySeconds == minPerSec)
        {
            CurrentTimeSpeedFactorBySeconds = hrPerSec;
            TimeScaleText.text = "1 hr/s";
        }
        else if (CurrentTimeSpeedFactorBySeconds == hrPerSec)
        {
            CurrentTimeSpeedFactorBySeconds = dayPerSec;
            TimeScaleText.text = "1 day/s";
        }
        else if (CurrentTimeSpeedFactorBySeconds == dayPerSec)
        {
            CurrentTimeSpeedFactorBySeconds = secPerSec;
            TimeScaleText.text = "1 sec/s";
        }
    }
}