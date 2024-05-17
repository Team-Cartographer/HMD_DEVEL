using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class ProcedureScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TitlePanel;
    public PressableButton TitleButton;
    public TMP_Text HPTitle;

    public GameObject MenuPanel;
    public TMP_Text EgressButton;
    public TMP_Text GeoButton;
    public TMP_Text RepairButton;
    public TMP_Text IngressButton;
    public TMP_Text BackMenuButton;

    public GameObject ProcedurePanel;
    public TMP_Text PreviousButtonText;
    public TMP_Text ExitButtonText;
    public TMP_Text NextButtonText;
    public PressableButton NextButton;
    public TMP_Text ProcedureText;
    // public PressableButton ExitButton;
    //public ConnectionHandler connectionHandler;

    private int prevID = 0;
    private int instructionNumber = 0;

    void Start()
    {
        DisplayTitlePanel();
        HPTitle.text = "PROCEDURES";
        EgressButton.text = "EGRESS";
        GeoButton.text = "GEO";
        RepairButton.text = "REPAIR";
        IngressButton.text = "INGRESS";
        BackMenuButton.text = "BACK";

        ExitButtonText.text = "EXIT";
        PreviousButtonText.text = "BACK";
        NextButtonText.text = "NEXT";


        NextButton.OnClicked.AddListener(IncrementInstruction);
    }

    // Update is called once per frame
    void Update()
    {

        Dictionary<int, string> egress = new Dictionary<int, string>();
        Dictionary<int, string> geo = new Dictionary<int, string>();
        Dictionary<int, string> repair = new Dictionary<int, string>();
        Dictionary<int, string> ingress = new Dictionary<int, string>();
        egress.Add(1, "");
        geo.Add(1, "EV Open Sampling Procedure");
        geo.Add(2, "If available, perform Field Notes on Rock, which can include picture, voice notes, etc.");
        geo.Add(3, "Perform XRF Scan");
        geo.Add(4, "Press and HOLD trigger");
        geo.Add(5, "Aim close to sample until beep, then release trigger");
        geo.Add(6, "If Rock Composition outside of nominal parameters(define), collect rock.");
        geo.Add(7, "If able, drop and label a pin");



        //TitleButton.onClick(DisplayMenuPanel());
        if (MenuPanel.tag != null && MenuPanel.tag == "egress")
        {
            ProcedureText.text = $"{instructionNumber}: {egress[instructionNumber]}";
        } 
        else if (MenuPanel.tag != null && MenuPanel.tag == "geo")
        {
            ProcedureText.text = $"{instructionNumber}: {geo[instructionNumber]}";
        } 
        else if (MenuPanel.tag != null && MenuPanel.tag == "repair")
        {
            ProcedureText.text = $"{instructionNumber}: {repair[instructionNumber]}";
        }
        else if (MenuPanel.tag != null && MenuPanel.tag == "ingress")
        {
            ProcedureText.text = $"{instructionNumber}: {repair[instructionNumber]}";
        }
    }

    void IncrementInstruction()
    {
        instructionNumber++;  // Increment the click counter
        Debug.Log("Button has been clicked " + instructionNumber + " times.");
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
