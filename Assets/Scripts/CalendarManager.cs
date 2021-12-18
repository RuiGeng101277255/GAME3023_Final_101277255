using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public DayDetailScript DayDetailDisplay;
    public GameSystemManager gameManager;

    public GameObject dayPrefab;

    public List<DayScript> currentMonthDayList;

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

    public void saveCurrentMonth()
    {
        if (currentMonthDayList.Count != 0)
        {
            CalendarContentSavingScript.Instance().SaveContents(gameManager.currentYear, gameManager.currentMonth, currentMonthDayList);
        }
    }

    public void DayClicked(DayScript day)
    {
        DayDetailDisplay.setTargetDateDetail(day);
        DayDetailDisplay.gameObject.SetActive(true);
    }

    public void setDaysAndContentsUsingString(string s)
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
                newDay.setDay(i, gameManager.currentMonth, gameManager.currentYear);

                for (int d = 1; d < details.Length; d++)
                {
                    newDay.DayDetails.Add(details[d]);
                }

                LoadedDays.Add(newDay);
            }
        }

        setDaysAndContents(LoadedDays);
    }

    public void setDaysAndContents(List<DayScript> days)
    {
        currentMonthDayList.Clear();

        foreach (DayScript d in days)
        {
            currentMonthDayList.Add(d);
        }

        populateCurrentMonth();
    }

    public void populateEmptyMonth()
    {
        List<DayScript> emptyDays = new List<DayScript>();

        for (int i = 1; i <= gameManager.maxMonthDay; i++)
        {
            GameObject newDayObj = Instantiate(dayPrefab);
            DayScript newDay = newDayObj.GetComponent<DayScript>();
            newDay.setDay(i, gameManager.currentMonth, gameManager.currentYear);
            emptyDays.Add(newDay);
        }
        setDaysAndContents(emptyDays);
    }

    public void ShowCurrentDayHighlighted(int dayNum)
    {
        if (dayNum > 1)
        {
            currentMonthDayList[dayNum - 2].setDayCurrentDisplay(false);
        }

        if (dayNum <= gameManager.maxMonthDay)
        {
            currentMonthDayList[dayNum - 1].setDayCurrentDisplay(true);
        }
    }
}