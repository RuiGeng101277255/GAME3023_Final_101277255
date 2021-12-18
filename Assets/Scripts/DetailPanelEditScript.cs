using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailPanelEditScript : MonoBehaviour
{
    public TMP_InputField DetailInput;
    public DayDetailScript detailPanel;

    public Toggle HolidayToggle;
    public Toggle BirthdayToggle;
    public Toggle LessonToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeEmptyMonthList()
    {

    }

    public void resetDetailPanel()
    {
        DetailInput.text = "";
    }

    public void AddDetails()
    {
        if (DetailInput.text != "")
        {
            detailPanel.targetDay.DayDetails.Add("- " + DetailInput.text);
            detailPanel.setTargetDateDetail(detailPanel.targetDay);
            gameObject.SetActive(false);
            detailPanel.gameObject.SetActive(true);
        }
    }

    public void TogglePressed()
    {
        detailPanel.targetDay.isDayHoliday = HolidayToggle.isOn;
        detailPanel.targetDay.isDayBirthday = BirthdayToggle.isOn;
        detailPanel.targetDay.isDayLesson = LessonToggle.isOn;
    }
}
