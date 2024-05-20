using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class HardProcedures : MonoBehaviour
{
    public GameObject mainPanel;
    public Button proceduresButton;

    public GameObject menuPanel;
    public Button egressButton;
    public Button geoButton;
    public Button repairButton;
    public Button ingressButton;
    public Button emergencyButton;
    public Button backButton;

    public GameObject procedurePanel;
    public TMP_Text procedureText;
    public Button exitButton;
    public Button nextButton;
    public Button previousButton;

    public GameObject emergencyPanel;
    public Button o2ErrorButton;
    public Button fanErrorButton;
    public Button abortMission;




    private Dictionary<string, List<string>> procedures = new Dictionary<string, List<string>>();
    private int currentInstructionIndex = 0;
    private string currentProcedure = "";

    void Start()
    {
        InitializeProcedures();
        ShowPanel(mainPanel);

        // StartProcedure("EMERGENCY")

        proceduresButton.onClick.AddListener(() => ShowPanel(menuPanel));
        emergencyButton.onClick.AddListener(() => ShowPanel(emergencyPanel));

        egressButton.onClick.AddListener(() => StartProcedure("EGRESS"));
        geoButton.onClick.AddListener(() => StartProcedure("GEO"));
        repairButton.onClick.AddListener(() => StartProcedure("REPAIR"));
        ingressButton.onClick.AddListener(() => StartProcedure("INGRESS"));

        o2ErrorButton.onClick.AddListener(() => StartProcedure("O2ERROR"));
        fanErrorButton.onClick.AddListener(() => StartProcedure("FANERROR"));
        abortMission.onClick.AddListener(() => StartProcedure("ABORT"));


        backButton.onClick.AddListener(() => ShowPanel(mainPanel));

        exitButton.onClick.AddListener(() => ShowPanel(mainPanel));
        nextButton.onClick.AddListener(NextInstruction);
        previousButton.onClick.AddListener(PreviousInstruction);

    }

    void InitializeProcedures()
    {
        procedures.Add("EGRESS", new List<string> {"1. EV1 and EV2 connect UIA and DCU umbilical",
            "2. EV-1, EV-2 PWR – ON", 
            "3. BATT – UMB", 
            "4. DEPRESS PUMP PWR – ON", 
            "4. OXYGEN O2 VENT – OPEN", 
            "5. Wait until both Primary and Secondary OXY tanks are < 10psi", 
            "6. OXYGEN O2 VENT – CLOSE", 
            "7. OXY – PRI", "" +
            "8. OXYGEN EMU-1, EMU-2 – OPEN", 
            "9. Wait until EV1 and EV2 Primary O2 tanks > 3000 psi", 
            "10. OXYGEN EMU-1, EMU-2 – CLOSE", 
            "11. OXY – SEC", 
            "12. OXYGEN EMU-1, EMU-2 – OPEN", 
            "13. Wait until EV1 and EV2 Secondary O2 tanks > 3000 psi", 
            "14. OXYGEN EMU-1, EMU-2 – CLOSE",
            "15. OXY – PRI Prep Water Tanks",
            "1. PUMP – OPEN", 
            "2. EV-1, EV-2 WASTE WATER – OPEN  3. Wait until water EV1 and EV2 Coolant tank is < 5% UIA 4. EV-1, EV-2 WASTE WATER – CLOSE UIA 5. EV-1, EV-2 SUPPLY WATER – OPEN HMD 6. Wait until water EV1 and EV2 Coolant tank is > 95% UIA 7. EV-1, EV-2 SUPPLY WATER – CLOSE BOTH DCU 8. PUMP – CLOSE END Depress, Check Switches and Disconnect HMD 1. Wait until SUIT P, O2 P = 4 UIA 2. DEPRESS PUMP PWR – OFF BOTH DCU 3. BATT – LOCAL UIA 9. EV-1, EV-2 PWR - OFF BOTH DCU 4. Verify OXY – PRI BOTH DCU 5. Verify COMMS – A BOTH DCU 6. Verify FAN – PRI BOTH DCU 7. Verify PUMP – CLOSE BOTH DCU 8. Verify CO2 – AUIA and DCU 9. EV1 and EV2 disconnect UIA and DCU umbilica" });

        procedures.Add("GEO", new List<string> { "1. If available, perform Field Notes on Rock, which can include picture, voice notes, etc.",
                                                "2. Press and HOLD XRF trigger",
                                                "3. Aim close to sample until beep, then release trigger",
                                                "4. If Rock Composition outside of nominal parameters, collect rock",
                                                "5. If able, drop and label a pin" });

        procedures.Add("REPAIR", new List<string> { "Repair step 1", "Repair step 2" });
        procedures.Add("INGRESS", new List<string> { "Ingress step 1", "Ingress step 2" });

        procedures.Add("O2ERROR", new List<string> { "Emergency step 1", "Emergency Step 2" });
        procedures.Add("FANERROR", new List<string> { "Emergency step 1", "Emergency Step 2" });
        procedures.Add("ABORT", new List<string> { "Emergency step 1", "Emergency Step 2" });
    }

    void ShowPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        menuPanel.SetActive(false);
        emergencyPanel.SetActive(false);
        procedurePanel.SetActive(false);

        panel.SetActive(true);
    }

    void StartProcedure(string procedure)
    {
        currentProcedure = procedure;
        currentInstructionIndex = 0;
        UpdateProcedureText();
        ShowPanel(procedurePanel);
    }

    void NextInstruction()
    {
        if (currentInstructionIndex < procedures[currentProcedure].Count - 1)
        {
            currentInstructionIndex++;
            UpdateProcedureText();
        }
    }

    void PreviousInstruction()
    {
        if (currentInstructionIndex > 0)
        {
            // Debug.Log("Previous Button Clicked ----------------------------------------------------------------");
            currentInstructionIndex--;
            // Debug.Log($"Current Instruction Index: {currentInstructionIndex}");
            UpdateProcedureText();
            // Debug.Log($"Current Procedure: {procedures[currentProcedure][currentInstructionIndex]}");
            ShowPanel(procedurePanel);
        } else if (currentInstructionIndex == 0)
        {
            ShowPanel(procedurePanel);
        }
    }

    void UpdateProcedureText()
    {
        if (procedures.ContainsKey(currentProcedure) && procedures[currentProcedure].Count > currentInstructionIndex)
        {
            procedureText.text = procedures[currentProcedure][currentInstructionIndex];
        }
    }
}
