using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The calendar manager that is the center of how the days are populated for each month and loading/saving their corresponding behaviours
public class CalendarManager : MonoBehaviour
{
    //Detail display that will show the specific day's details and components
    public DayDetailScript DayDetailDisplay;
    public GameSystemManager gameManager; //game manager for the tracking of current times, and which months of the calendar the player would like to open.

    public GameObject dayPrefab; //prefab for the dayscript game objects, will need as many as the number of days within the chosen month
    public List<DayScript> currentMonthDayList; //List of days within the month this calendar is showing

    //Information about the specific month of specific year the calendar is currently displaying
    public int CalendarDisplayMonth;
    public int CalendarDisplayYear;

    public PlayerBehaviour player;
    void populateCurrentMonth()
    {
        //Cleans the days in the month if it has any, so that the new month's objects can be added
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        //If the list of days isn't empty, then populate it.
        if (currentMonthDayList.Count != 0)
        {
            foreach (DayScript d in currentMonthDayList)
            {
                d.setCalendarManager(this);
                d.transform.SetParent(transform);
            }
        }
    }

    public void setButtonInteraction(bool b)
    {
        //Disables all the day's button behaviour after one is open
        var allDays = FindObjectsOfType<DayScript>();

        foreach (DayScript d in allDays)
        {
            d.GetComponent<Button>().interactable = b;
        }
    }

    public void saveCurrentMonth()
    {
        //Saves current month's information so that it can be loaded later
        if (currentMonthDayList.Count != 0)
        {
            CalendarContentSavingScript.Instance().SaveContents(CalendarDisplayYear, CalendarDisplayMonth, currentMonthDayList);
        }
    }

    public void DayClicked(DayScript day)
    {
        //The behaviour when a day is clicked (to show its details)
        DayDetailDisplay.setTargetDateDetail(day);
        DayDetailDisplay.gameObject.SetActive(true);
        setButtonInteraction(false);

        //Disables the player movement if the day is clicked
        player.isCalendarInDetails = true;
    }

    public void setDaysAndContentsUsingString(int year, int month, string s)
    {
        //Sets the list of days when we get a string from the specific loadable month

        //Populates a temporal list of days
        List<DayScript> LoadedDays = new List<DayScript>();
        string[] dayNdetails = s.Split(SaveLoadSignifiers.DaySeparator.ToCharArray()[0]);

        for (int i = 0; i < dayNdetails.Length; i++)
        {
            if ((dayNdetails[i] != "") && (dayNdetails[i] != null))
            {
                string[] details = dayNdetails[i].Split(SaveLoadSignifiers.DetailSeparator.ToCharArray()[0]);

                GameObject newDayObj = Instantiate(dayPrefab);
                DayScript newDay = newDayObj.GetComponent<DayScript>();
                newDay.setDay(i, month, year);

                newDay.isDayHoliday = bool.Parse(details[1]);
                newDay.isDayBirthday = bool.Parse(details[2]);
                newDay.isDayLesson = bool.Parse(details[3]);

                for (int d = 4; d < details.Length; d++)
                {
                    newDay.DayDetails.Add(details[d]);
                }

                LoadedDays.Add(newDay);
            }
        }

        //After the temporal list of days is populated, we set its contents in the calendar
        setDaysAndContents(year, month, LoadedDays);
    }

    public void setDaysAndContents(int year, int month, List<DayScript> days)
    {
        //Sets the days for the specific month and year the calendar is currently displaying
        CalendarDisplayYear = year;
        CalendarDisplayMonth = month;

        //clears the list of the existing days before adding the new ones
        currentMonthDayList.Clear();

        foreach (DayScript d in days)
        {
            currentMonthDayList.Add(d);
        }

        populateCurrentMonth();
    }

    public void populateEmptyMonth(int year, int month)
    {
        //similar to loading an existing list of days but this function makes a bunch of empty days
        //Used if there isn't any available or loadable data

        int maxTargetMonthDays = MonthStructureScript.Instance().getTargetMonthMaxDayNumber(year, month);

        List<DayScript> emptyDays = new List<DayScript>();

        for (int i = 1; i <= maxTargetMonthDays; i++)
        {
            GameObject newDayObj = Instantiate(dayPrefab);
            DayScript newDay = newDayObj.GetComponent<DayScript>();
            newDay.setDay(i, month, year);
            emptyDays.Add(newDay);
        }
        setDaysAndContents(year, month, emptyDays);
    }

    public void ShowCurrentDayHighlighted()
    {
        //Highlights the player's current/present day if available
        foreach (DayScript day in currentMonthDayList)
        {
            if (((day.dayNumber == gameManager.currentDay) && (day.dayMonth == gameManager.currentMonth)) && (day.dayYear == gameManager.currentYear))
            {
                day.setDayCurrentDisplay(true);
            }
            else
            {
                day.setDayCurrentDisplay(false);
            }
        }
    }
}