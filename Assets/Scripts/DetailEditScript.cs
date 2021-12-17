using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailEditScript : MonoBehaviour
{
    public DetailPanelEditScript EditPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditToggled()
    {
        bool isPanelOpen = EditPanel.gameObject.activeSelf;

        if (!isPanelOpen)
        {
            EditPanel.resetDetailPanel();
        }

        EditPanel.gameObject.SetActive(!isPanelOpen);
    }
}
