using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributionsButton : MonoBehaviour
{
    public GameObject AttributionsPanel;
    private bool PanelStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TogglePanel()
    {
        PanelStatus = !PanelStatus;
        AttributionsPanel.SetActive(PanelStatus);
    }
    
}
