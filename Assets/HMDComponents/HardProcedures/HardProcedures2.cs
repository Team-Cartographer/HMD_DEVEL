using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardProcedures2 : MonoBehaviour
{
    public GameObject mainPanel;
    public Button proceduresButton;

    public GameObject menuPanel;
    public Button egressButton;
    public Button geoButton;
    public Button repairButton;
    public Button ingressButton;
    public Button backButton;

    public GameObject procedurePanel;
    public TMP_Text procedureText;
    public Button exitButton;
    public Button nextButton;
    public Button previousButton;

    private Dictionary<string, List<string>> procedures = new Dictionary<string, List<string>>();
    private int currentInstructionIndex = 0;
    private string currentProcedure = "";

    void Start()
    {
        InitializeProcedures();
        ShowPanel(mainPanel);

        proceduresButton.onClick.AddListener(() => ShowPanel(menuPanel));
        

        egressButton.onClick.AddListener(() => StartProcedure("EGRESS"));
        geoButton.onClick.AddListener(() => StartProcedure("GEO"));
        repairButton.onClick.AddListener(() => StartProcedure("REPAIR"));
        ingressButton.onClick.AddListener(() => StartProcedure("INGRESS"));
        backButton.onClick.AddListener(() => ShowPanel(mainPanel));

        exitButton.onClick.AddListener(() => ShowPanel(menuPanel));
        nextButton.onClick.AddListener(NextInstruction);
        previousButton.onClick.AddListener(PreviousInstruction);

    }

    void InitializeProcedures()
    {
        procedures.Add("EGRESS", new List<string> { "Egress step 1", "Egress step 2" });
        procedures.Add("GEO", new List<string> { "Geo step 1", "Geo step 2", "Geo step 3" });
        procedures.Add("REPAIR", new List<string> { "Repair step 1", "Repair step 2" });
        procedures.Add("INGRESS", new List<string> { "Ingress step 1", "Ingress step 2" });
    }

    void ShowPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        menuPanel.SetActive(false);
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
