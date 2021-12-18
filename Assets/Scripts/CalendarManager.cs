using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public DayDetailScript DayDetailDisplay;
    public GameSystemManager gameManager;

    public GameObject dayPrefab;

    public List<DayScript> currentMonthDayList;

    public int CalendarDisplayMonth;
    public int CalendarDisplayYear;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void populateCurrentMonth()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

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
        var allDays = FindObjectsOfType<DayScript>();

        foreach (DayScript d in allDays)
        {
            d.GetComponent<Button>().interactable = b;
        }
    }

    public void saveCurrentMonth()
    {
        if (currentMonthDayList.Count != 0)
        {
            int targetYear = currentMonthDayList[0].dayYear;
            int targetMonth = currentMonthDayList[0].dayMonth;
            CalendarContentSavingScript.Instance().SaveContents(targetYear, targetMonth, currentMonthDayList);
        }
    }

    public void DayClicked(DayScript day)
    {
        DayDetailDisplay.setTargetDateDetail(day);
        DayDetailDisplay.gameObject.SetActive(true);
        setButtonInteraction(false);
    }

    public void setDaysAndContentsUsingString(int year, int month, string s)
    {
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

                for (int d = 1; d < details.Length; d++)
                {
                    newDay.DayDetails.Add(details[d]);
                }

                LoadedDays.Add(newDay);
            }
        }

        setDaysAndContents(year, month, LoadedDays);
    }

    public void setDaysAndContents(int year, int month, List<DayScript> days)
    {
        CalendarDisplayYear = year;
        CalendarDisplayMonth = month;

        currentMonthDayList.Clear();

        foreach (DayScript d in days)
        {
            currentMonthDayList.Add(d);
        }

        populateCurrentMonth();
    }

    public void populateEmptyMonth(int year, int month)
    {
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


        //if ((monthNum == gameManager.currentMonth) && (yearNum == gameManager.currentYear))
        //{
        //    foreach (DayScript day in currentMonthDayList)
        //    {
        //
        //    }
        //    if (dayNum > 1)
        //    {
        //        currentMonthDayList[dayNum - 2].setDayCurrentDisplay(false);
        //    }
        //
        //    if (dayNum <= gameManager.maxMonthDay)
        //    {
        //        currentMonthDayList[dayNum - 1].setDayCurrentDisplay(true);
        //    }
        //}
    }
}