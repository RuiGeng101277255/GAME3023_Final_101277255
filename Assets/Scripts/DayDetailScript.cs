using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//For the detail display of each day (happens when the player clicks on the specific slot in the calendar)
public class DayDetailScript : MonoBehaviour
{
    public Text DetailTitle; //Title to display the date
    public GameObject DetailListPanel; //Panel to display the chosen date's details

    public GameObject TextPrefab; //Prefab of the detail texts
    public DayScript targetDay; //The day in question

    void ClearDisplayContents()
    {
        //Clears every detail text within the panel
        DetailTitle.text = "";

        for (int i = 0; i < DetailListPanel.transform.childCount; i++)
        {
            Destroy(DetailListPanel.transform.GetChild(i).gameObject);
        }
    }

    public void setTargetDateDetail(DayScript day)
    {
        //Sets the correct UI display of the dates and their corresponding details
        ClearDisplayContents();
        targetDay = day;
        DetailTitle.text = day.dayMonth + "/" + day.dayNumber  + "/" + day.dayYear;

        foreach (string s in day.DayDetails)
        {
            GameObject tempDetail = Instantiate(TextPrefab);
            tempDetail.transform.SetParent(DetailListPanel.transform);
            tempDetail.GetComponent<Text>().text = s;
        }
    }
}
