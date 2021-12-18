using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script for the detail edit panel, this is displayed so that the player can make detail additions
public class DetailPanelEditScript : MonoBehaviour
{
    public CalendarManager calendarManager;

    //Gets the inputfield and the detail panel gameobject that displays any chosen days details.
    public TMP_InputField DetailInput;
    public DayDetailScript detailPanel;

    public Toggle HolidayToggle; //Checks if the day should be a holiday
    public Toggle BirthdayToggle; //Checks if the day should be a birthday
    public Toggle LessonToggle; //Checks if the day should have a lesson

    public void resetDetailPanel()
    {
        //Resets the inputfield
        DetailInput.text = "";
        HolidayToggle.isOn = detailPanel.targetDay.isDayHoliday;
        BirthdayToggle.isOn = detailPanel.targetDay.isDayBirthday;
        LessonToggle.isOn = detailPanel.targetDay.isDayLesson;
    }

    public void AddDetails()
    {
        //If the inputfield is not empty, then add the corresponding texts to the details within the chosen day
        if (DetailInput.text != "")
        {
            detailPanel.targetDay.DayDetails.Add("- " + DetailInput.text);
            detailPanel.setTargetDateDetail(detailPanel.targetDay);
            gameObject.SetActive(false);
            detailPanel.gameObject.SetActive(true);
            calendarManager.saveCurrentMonth(); //Saves when the player make an edit
        }
    }

    public void TogglePressed()
    {
        //Each time a toggle is pressed, it sets special icon components to the toggles' conditions
        detailPanel.targetDay.isDayHoliday = HolidayToggle.isOn;
        detailPanel.targetDay.isDayBirthday = BirthdayToggle.isOn;
        detailPanel.targetDay.isDayLesson = LessonToggle.isOn;
        calendarManager.saveCurrentMonth(); //Saves when the player make an edit
    }
}
