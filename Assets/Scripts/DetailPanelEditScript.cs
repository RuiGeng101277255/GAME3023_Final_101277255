using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailPanelEditScript : MonoBehaviour
{
    public TMP_InputField DetailInput;
    public DayDetailScript detailPanel;

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
        }
    }
}
