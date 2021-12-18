using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSystemManager : MonoBehaviour
{
    public TMP_Text TimeText;
    public TMP_Text CalendarText;
    public CalendarManager calendarManager;

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

        MonthPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeValue();
        updateTimeText();
        updateCalendarValue();
        updateCalendarText();
    }

    void MonthPopulation()
    {
        string targetDaysString = CalendarContentSavingScript.Instance().LoadStringContentsByMonthAndYear(currentYear, currentMonth);
        if (targetDaysString != "")
        {
            calendarManager.setDaysAndContentsUsingString(targetDaysString);
        }
        else
        {
            calendarManager.populateEmptyMonth();
        }

        calendarManager.ShowCurrentDayHighlighted(currentDay);
    }

    void updateTimeValue()
    {
        currentTime_Seconds += Time.deltaTime;

        if (currentTime_Seconds >= 60.0f)
        {
            currentTime_Minute++;
            currentTime_Seconds -= 60.0f;
        }

        if (currentTime_Minute >= 60)
        {
            currentTime_Hour++;
            currentTime_Minute -= 60;
        }

        if (currentTime_Hour >= 24)
        {
            currentDay++;
            currentTime_Hour -= 24;
            calendarManager.ShowCurrentDayHighlighted(currentDay);
        }
    }

    void updateTimeText()
    {
        TimeText.text = currentTime_Hour.ToString("00") + " Hr " + currentTime_Minute.ToString("00") + " Min " + ((int)currentTime_Seconds).ToString("00") + " Sec ";
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
        MonthStructure MonthInQuestion = new MonthStructure();
        MonthName = MonthInQuestion.getMonthStructure(currentMonth, isLeapYear).MonthName;
        maxMonthDay = MonthInQuestion.getMonthStructure(currentMonth, isLeapYear).MonthMaxDay;
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
            MonthPopulation();
        }
    }

    void updateCalendarText()
    {
        string dayPostfix = "";
        int lastDayDigit = currentDay % 10;
        int secondDayDigit = (int)(currentDay / 10);

        if (secondDayDigit != 1)
        {
            switch (lastDayDigit)
            {
                case 1:
                    dayPostfix = "st";
                    break;
                case 2:
                    dayPostfix = "nd";
                    break;
                case 3:
                    dayPostfix = "rd";
                    break;
                default:
                    dayPostfix = "th";
                    break;
            }
        }
        else
        {
            dayPostfix = "th";
        }

        CalendarText.text = MonthName + " " + currentDay + dayPostfix + "," + currentYear;
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
        if (isNext)
        {
            currentMonth++;
        }
        else
        {
            currentMonth--;
        }

        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }
        else if (currentMonth < 1)
        {
            currentMonth = 12;
            currentYear--;
        }

        updateMonthValue();
        currentTime_Hour = 0;
        currentTime_Minute = 0;
        currentTime_Seconds = 0.0f;
        currentDay = 1;

        MonthPopulation();
    }
}

public class MonthStructure
{
    public string MonthName;
    public int MonthMaxDay;

    public MonthStructure getMonthStructure(int monthNumber, bool isLeap)
    {
        MonthStructure targetMonth = new MonthStructure();

        switch (monthNumber)
        {
            case 1:
                targetMonth.MonthName = "January";
                targetMonth.MonthMaxDay = 31;
                break;
            case 2:
                targetMonth.MonthName = "February";
                targetMonth.MonthMaxDay = (isLeap) ? 29 : 28;
                break;
            case 3:
                targetMonth.MonthName = "March";
                targetMonth.MonthMaxDay = 31;
                break;
            case 4:
                targetMonth.MonthName = "April";
                targetMonth.MonthMaxDay = 30;
                break;
            case 5:
                targetMonth.MonthName = "May";
                targetMonth.MonthMaxDay = 31;
                break;
            case 6:
                targetMonth.MonthName = "June";
                targetMonth.MonthMaxDay = 30;
                break;
            case 7:
                targetMonth.MonthName = "July";
                targetMonth.MonthMaxDay = 31;
                break;
            case 8:
                targetMonth.MonthName = "August";
                targetMonth.MonthMaxDay = 31;
                break;
            case 9:
                targetMonth.MonthName = "September";
                targetMonth.MonthMaxDay = 30;
                break;
            case 10:
                targetMonth.MonthName = "October";
                targetMonth.MonthMaxDay = 31;
                break;
            case 11:
                targetMonth.MonthName = "November";
                targetMonth.MonthMaxDay = 30;
                break;
            case 12:
                targetMonth.MonthName = "December";
                targetMonth.MonthMaxDay = 31;
                break;
        }

        return targetMonth;
    }
}