using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

//Manages the game system and time, and works side by side with the calendar manager
public class GameSystemManager : MonoBehaviour
{
    public TMP_Text CurrentTimeText; //Shows the current time and date
    public TMP_Text CalendarText; //Shows the date of the calendar that is opened
    public CalendarManager calendarManager; //Calendar Manager in the game

    //Sets the scale and behaviour of how the time passing
    public TMP_Text TimeScaleText;
    private float secPerSec = 1.0f;
    private float minPerSec = 60.0f; // 60 seconds in a min
    private float hrPerSec = 3600.0f; // 3600 seconds in an hour
    private float dayPerSec = 86400.0f; // 86400 seconds in a day
    private float CurrentTimeSpeedFactorBySeconds; //the current scale factor
    private float totalRunTime = 0.0f; //Total run time for the lights to behave properly
    //Ability for the player to set their own custom time scale
    private bool isPlayerCustomTimeScale = false;
    private float playerCustomTimeScaleFactorBySeconds = 0.0f;

    //Global light so that the game goes dark at night and bright in the morning
    public Light2D GlobalDayLight;
    private float initialDayLightIntensity;

    //Date Informations
    public int currentDay;
    public int maxMonthDay;
    public int currentMonth;
    public string MonthName;
    public int currentYear;
    public bool isLeapYear;
    //Time Informations
    private int currentTime_Hour;
    private int currentTime_Minute;
    private float currentTime_Seconds;

    // Start is called before the first frame update
    void Start()
    {
        //Initial values for the variables
        //Values for the day
        currentTime_Hour = 0;
        currentTime_Minute = 0;
        currentTime_Seconds = 0.0f;

        //Initial time scale factor
        CurrentTimeSpeedFactorBySeconds = 1.0f;

        //Initial Month upon loading the game
        MonthPopulation(currentYear, currentMonth);
        //Sets the initial daylight intensity so we know the value for the brightest during daytime
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
        //Loads from the calendarcontentsaving singleton

        string targetDaysString = CalendarContentSavingScript.Instance().LoadStringContentsByMonthAndYear(year, month);
        if (targetDaysString != "")
        {
            //If there is available data then ask the calendar manager load those
            calendarManager.setDaysAndContentsUsingString(year, month, targetDaysString);
        }
        else
        {
            //If there isn't any available data, then ask the calendar manager to make an empty one
            calendarManager.populateEmptyMonth(year, month);
        }

        //Make sure we highlight the correct day (for the player's present day)
        calendarManager.ShowCurrentDayHighlighted();
    }

    void updateTimeValue()
    {
        //Updates the values for the time

        currentTime_Seconds += Time.deltaTime * CurrentTimeSpeedFactorBySeconds;

        //Note: the below while loops are used to make sure the behaviour of the time passing, especially for large speed scales (passing very fast)

        //If the second is or more than 60, then subtract it until it's less than that, and add the correct amount to the minutes
        if (currentTime_Seconds >= 60.0f)
        {
            while (currentTime_Seconds >= 60.0f)
            {
                currentTime_Minute++;
                currentTime_Seconds -= 60.0f;
            }
        }

        //If the minutes is or more than 60, then subtract it until it's less than that, and add the correct amount to the hours
        if (currentTime_Minute >= 60)
        {
            while (currentTime_Minute >= 60)
            {
                currentTime_Hour++;
                currentTime_Minute -= 60;
            }
        }

        //If the hours is or more than 24, then subtract it until it's less than that, and add the correct amount to the days
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
        //Updates the UI text
        CurrentTimeText.text = MonthName + " " 
            + currentDay + MonthStructureScript.Instance().getDayPostFix(currentDay) + "," 
            + currentYear + " " + currentTime_Hour.ToString("00") + ": " 
            + currentTime_Minute.ToString("00") + ": " 
            + ((int)currentTime_Seconds).ToString("00");
    }

    void updateDayLightColor()
    {
        //Updates the global light color based on runtime value
        float runtimeDaySpeedInSeconds = CurrentTimeSpeedFactorBySeconds / 43200.0f; //half a day
        totalRunTime += Time.deltaTime * runtimeDaySpeedInSeconds;
        GlobalDayLight.intensity = Mathf.Lerp(0.0f, initialDayLightIntensity, Mathf.PingPong(totalRunTime, 1.0f)); ;
    }

    void updateCalendarValue()
    {
        //Checks calendar value for any changes and updates them.
        checkForLeapYear();
        updateMonthValue();
        updateDayValue();
    }

    void checkForLeapYear()
    {
        //Check if the year is a leap year, happens every 4 years
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
        //Updates the month's name and max number of days it has by using a singleton of monthstructurescript
        MonthName = MonthStructureScript.Instance().getTargetMonthName(currentMonth);
        maxMonthDay = MonthStructureScript.Instance().getTargetMonthMaxDayNumber(currentYear, currentMonth);
    }

    void updateDayValue()
    {
        //Updates the days so that if it's more than the max day, then the month is over
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
        //Updates the UI text for the month the calendar is currently displaying
        //Can be different from the actual player's present time
        CalendarText.text = MonthStructureScript.Instance().getTargetMonthName(calendarManager.CalendarDisplayMonth) + " of " + calendarManager.CalendarDisplayYear;
    }

    public void setTime(int hour, int minute, int seconds)
    {
        //Sets the game present time values
        currentTime_Hour = hour;
        currentTime_Minute = minute;
        currentTime_Seconds = (float)seconds;
    }

    public void setDate(int year, int month, int day)
    {
        //Sets the game present day values
        currentYear = year;
        currentMonth = month;
        currentDay = day;
    }

    public void monthChangePressed(bool isNext)
    {
        //For when the player changes the month to one different from the one displaying
        //Goes in a sequence fashion
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

        MonthPopulation(targetYear, targetMonth);
    }

    public void TimeScalePressed()
    {
        //Changes the time scale based on the current one
        //Allows the time to pass slower/faster
        if (!isPlayerCustomTimeScale)
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
            else
            {
                CurrentTimeSpeedFactorBySeconds = secPerSec;
                TimeScaleText.text = "1 sec/s";
            }
        }
        else
        {
            CurrentTimeSpeedFactorBySeconds = playerCustomTimeScaleFactorBySeconds;
            TimeScaleText.text = "Custome";
        }
    }

    public void setPlayerCustomeTimeScaleBySeconds(float scale, bool isCustom)
    {
        //A custom setter for the time scale if the developers want to have a specific time flow speed
        playerCustomTimeScaleFactorBySeconds = scale;
        isPlayerCustomTimeScale = isCustom;
    }
}