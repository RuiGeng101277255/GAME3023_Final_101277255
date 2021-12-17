using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayScript : MonoBehaviour
{
    public int dayNumber;
    public List<string> DayDetails;

    private CalendarManager Calendar;

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
}
