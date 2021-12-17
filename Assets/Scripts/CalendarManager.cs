using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public GameObject DayPrefab;
    public DayDetailScript DayDetailDisplay;

    public List<DayScript> DayList;

    // Start is called before the first frame update
    void Start()
    {
        DayList[0].setCalendarManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DayClicked(DayScript day)
    {
        DayDetailDisplay.setTargetDateDetail(day);
        DayDetailDisplay.gameObject.SetActive(true);
    }
}
