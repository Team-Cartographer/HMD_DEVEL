using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ProcedureScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TitlePanel;
    public PressableButton TitleButton;
    public TMP_Text HPTitle;

    public GameObject MenuPanel;
    // public PressableButton EgressButton;
    // public PressableButton GeoButton;
    // public PressableButton RepairButton;
    // public PressableButton IngressButton;

    public GameObject ProcedurePanel;
    // public PressableButton BackButton;
    // public PressableButton PreviousButton;
    // public PressableButton NextButton;
    // public PressableButton ExitButton;
    //public ConnectionHandler connectionHandler;

    private int prevID = 0;

    void Start()
    {
        // DisplayTitlePanel();
        HPTitle.text = "PROCEDURES";
        // EgressButton.text = "EGRESS";
        // GeoButton.text = "GEOLOGICAL SAMPLING";
        // RepairButton.text = "REPAIR";
        // IngressButton.text = "INGRESS";
    }

    // Update is called once per frame
    void Update()
    {
        //TitleButton.onClick(DisplayMenuPanel());
    }

    void DisplayTitlePanel()
    {
        TitlePanel.SetActive(true);
        MenuPanel.SetActive(false);
        ProcedurePanel.SetActive(false);
    }

    void DisplayMenuPanel()
    {
        MenuPanel.SetActive(true);
        TitlePanel.SetActive(false);
        ProcedurePanel.SetActive(false);
    }

    void DisplayProcedurePanel()
    {
        ProcedurePanel.SetActive(true);
        TitlePanel.SetActive(false);
        MenuPanel.SetActive(false);
    }
}
