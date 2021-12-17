using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayDetailScript : MonoBehaviour
{
    public Text DetailTitle;
    public GameObject DetailListPanel;

    public GameObject TextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearDisplayContents()
    {
        DetailTitle.text = "";

        for (int i = 0; i < DetailListPanel.transform.childCount; i++)
        {
            Destroy(DetailListPanel.transform.GetChild(i).gameObject);
        }
    }

    public void setTargetDateDetail(DayScript day)
    {
        ClearDisplayContents();
        DetailTitle.text = "Day " + day.dayNumber + ":";

        foreach (string s in day.DayDetails)
        {
            GameObject tempDetail = Instantiate(TextPrefab);
            tempDetail.transform.SetParent(DetailListPanel.transform);
            tempDetail.GetComponent<Text>().text = s;
        }
    }
}
