using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayScript : MonoBehaviour
{
    public TMP_Text DayText;
    public List<string> DayDetails;
    public GameObject selectionImage;

    //Icons
    public GameObject DayHasDetailIcon;
    public GameObject HolidayIcon;
    public GameObject BirthdayIcon;
    public GameObject WitchCraftLessonIcon;

    private CalendarManager Calendar;
    public int dayNumber;
    public int dayMonth;
    public int dayYear;

    public bool isDayHoliday = false;
    public bool isDayBirthday = false;
    public bool isDayLesson = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateIconDisplay();
    }

    void updateIconDisplay()
    {
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
        Calendar.DayClicked(this);
    }

    public void setDay(int dayNum, int monthNum, int yearNum)
    {
        dayNumber = dayNum;
        dayMonth = monthNum;
        dayYear = yearNum;
        DayText.text = dayNum + "";
    }

    public void setDayCurrentDisplay(bool isThisDayCurrent)
    {
        selectionImage.SetActive(isThisDayCurrent);
    }
}