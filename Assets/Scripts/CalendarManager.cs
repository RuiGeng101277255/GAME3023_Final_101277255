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
        if (currentMonthDayList.Count != 0)
        {
            foreach (DayScript d in currentMonthDayList)
            {
                d.setCalendarManager(this);
                d.transform.SetParent(transform);
            }
        }
    }

    void saveCurrentMonth()
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

    public void setDaysAndContents(List<DayScript> days)
    {
        saveCurrentMonth();

        currentMonthDayList.Clear();

        foreach (DayScript d in days)
        {
            currentMonthDayList.Add(d);
        }

        populateCurrentMonth();
    }

    public void populateFirstMonth()
    {
        currentMonthDayList.Clear();

        for (int i = 1; i <= gameManager.maxMonthDay; i++)
        {
            GameObject newDayObj = Instantiate(dayPrefab);
            DayScript newDay = newDayObj.GetComponent<DayScript>();
            newDay.setDayNumber(i);
            currentMonthDayList.Add(newDay);
        }

        populateCurrentMonth();
        currentMonthDayList[0].setDayCurrentDisplay(true);
    }

    public void ShowCurrentDayHighlighted(int dayNum)
    {
        if (dayNum > 1)
        {
            currentMonthDayList[dayNum - 2].setDayCurrentDisplay(false);
        }
        currentMonthDayList[dayNum - 1].setDayCurrentDisplay(true);
    }
}
