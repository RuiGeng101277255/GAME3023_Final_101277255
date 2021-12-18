using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayScript : MonoBehaviour
{
    public TMP_Text DayText;
    public List<string> DayDetails;
    public GameObject selectionImage;

    private CalendarManager Calendar;
    public int dayNumber;
    public int dayMonth;
    public int dayYear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
