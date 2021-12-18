using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the detail edits, this calls the detail edit panel (which will do the actual edits)
public class DetailEditScript : MonoBehaviour
{
    //Gets the edit panel game object, this is used so that detail edit game object can turn on the panel for actual edits.
    //Think of this as the "manager" of the panel
    public DetailPanelEditScript EditPanel;

    public void EditToggled()
    {
        //Sets the panel depending on the state of the toggle
        //If panel is just opened (by the toggle and wasn't open before) then it resets the detail panel

        bool isPanelOpen = EditPanel.gameObject.activeSelf;

        if (!isPanelOpen)
        {
            EditPanel.resetDetailPanel();
        }

        EditPanel.gameObject.SetActive(!isPanelOpen);
    }
}
