using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailPanelEditScript : MonoBehaviour
{
    public TMP_InputField YearInput;
    public TMP_InputField MonthInput;
    public TMP_InputField DayInput;
    public TMP_InputField DetailInput;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetDetailPanel()
    {
        YearInput.text = "";
        MonthInput.text = "";
        DayInput.text = "";
        DetailInput.text = "";
    }

    public void AddDetails()
    {
        if ((((YearInput.text != "") && (MonthInput.text != "")) && (DayInput.text != "")) && (DetailInput.text != ""))
        {
            int yearNumber = int.Parse(YearInput.text);
            int MonthNumber = int.Parse(MonthInput.text);
            int DayNumber = int.Parse(DayInput.text);
        }
    }
}
