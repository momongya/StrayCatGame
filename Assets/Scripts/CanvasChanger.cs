using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject rulePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScreenDisplay()
    {
        rulePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void HideScreen()
    {
        rulePanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
