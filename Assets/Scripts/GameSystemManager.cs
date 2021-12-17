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

        currentDay = 1;
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            setTime(23, 59, 59);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            setDate(2000, 2, 28);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CalendarContentSavingScript.Instance().SaveContents(2001, 11, null);
        }
    }

    void MonthPopulation()
    {
        List<DayScript> InitialDays = CalendarContentSavingScript.Instance().LoadContentsByMonthAndYear(currentYear, currentMonth);

        if (InitialDays != null)
        {
            calendarManager.setDaysAndContents(InitialDays);
        }
        else
        {
            calendarManager.populateEmptyMonth();
        }
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
        switch (currentMonth)
        {
            case 1:
                MonthName = "January";
                maxMonthDay = 31;
                break;
            case 2:
                MonthName = "February";
                maxMonthDay = (isLeapYear) ? 29 : 28;
                break;
            case 3:
                MonthName = "March";
                maxMonthDay = 31;
                break;
            case 4:
                MonthName = "April";
                maxMonthDay = 30;
                break;
            case 5:
                MonthName = "May";
                maxMonthDay = 31;
                break;
            case 6:
                MonthName = "June";
                maxMonthDay = 30;
                break;
            case 7:
                MonthName = "July";
                maxMonthDay = 31;
                break;
            case 8:
                MonthName = "August";
                maxMonthDay = 31;
                break;
            case 9:
                MonthName = "September";
                maxMonthDay = 30;
                break;
            case 10:
                MonthName = "October";
                maxMonthDay = 31;
                break;
            case 11:
                MonthName = "November";
                maxMonthDay = 30;
                break;
            case 12:
                MonthName = "December";
                maxMonthDay = 31;
                break;
        }
    }

    void updateDayValue()
    {
        if (currentDay >= maxMonthDay)
        {
            if (currentMonth != 12)
            {
                currentMonth++;
            }
            else
            {
                currentMonth = 1;
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
}
