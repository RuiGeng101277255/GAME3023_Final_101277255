using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script for the day prefab that allows each to have their own unique identities
public class DayScript : MonoBehaviour
{
    //Details within each day that can be displayed on the calendar
    public TMP_Text DayText; //day text for the number
    public List<string> DayDetails; //details within the day
    public GameObject selectionImage; //highlights if it's the current day

    //Icons
    public GameObject DayHasDetailIcon; //coin if the day has something in it
    public GameObject HolidayIcon; //if the day is a holiday
    public GameObject BirthdayIcon; //if the day is someone's bday
    public GameObject WitchCraftLessonIcon; //if the player has witch craft lesson

    //Info to identify the day
    private CalendarManager Calendar; //calendar manager what will be the center of the diary calendar
    public int dayNumber; //the specific number of day this is
    public int dayMonth; //the specific number of month the day belongs to
    public int dayYear; //the specific number of year the day belongs to

    //For the display of the icons
    public bool isDayHoliday = false;
    public bool isDayBirthday = false;
    public bool isDayLesson = false;

    // Update is called once per frame
    void Update()
    {
        updateIconDisplay();
    }

    void updateIconDisplay()
    {
        //Updates the icon gameobjects activeness/visibility based on the day's criteria
        DayHasDetailIcon.SetActive(((DayDetails.Count != 0) ? true : false));
        HolidayIcon.SetActive(isDayHoliday);
        BirthdayIcon.SetActive(isDayBirthday);
        WitchCraftLessonIcon.SetActive(isDayLesson);
    }

    public void setCalendarManager(CalendarManager cM)
    {
        Calendar = cM;
    }

    public void DayClicked()
    {
        //Notifies the calendar that this specific day was clicked
        Calendar.DayClicked(this);
    }

    public void setDay(int dayNum, int monthNum, int yearNum)
    {
        //setter for the day's internal components
        dayNumber = dayNum;
        dayMonth = monthNum;
        dayYear = yearNum;
        DayText.text = dayNum + "";
    }

    public void setDayCurrentDisplay(bool isThisDayCurrent)
    {
        //for the highlight if it's the current day
        selectionImage.SetActive(isThisDayCurrent);
    }
}