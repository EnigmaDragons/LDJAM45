using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObjectSelector : MonoBehaviour
{
    public GameObject newActivePanel;

    void Start()
    {
        ShowPanels showPanelsScript = FindObjectOfType<ShowPanels>();

        if (showPanelsScript != null)
        {
            showPanelsScript.activePanel = newActivePanel;
        }      
    }
}
