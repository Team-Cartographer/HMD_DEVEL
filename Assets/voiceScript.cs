using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class voiceScript : MonoBehaviour
{
    // Script imports
    public WarningSystemScript warningSystemScript;
    public TelemetryScript telemetryScript;
    public CriticalTelemetry criticalTelemetryScript; // we should rename critical telemetry to critical telemetry script

    // Canvas imports
    public Canvas compassCanvas;
    public Canvas geoCanvas;

    // Keyword recognizer
    private KeywordRecognizer keywordRecognizer;

    // Start is called before the first frame update
    void Start()
    {
        keywordRecognizer = new KeywordRecognizer(new string[] {
            // warning system
            "warning on", "warning off",
            "to do on", "to do off",
            "task done",

            // telemetry
            "telemetry on", "telemetry off",
            "page one", "page two",
            "time on", "time off",

            // critical telemetry
            "critical on", "critical off",
            "bpm on", "bpm off",
            "battery life on", "battery life off",
            "oxygen life on", "oxygen life off",

            // compass
            "compass on", "compass off",

            // geo sampling
            "geo on", "geo off"

        });

        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Voice command: " + args.text);

        // warning system
        if (args.text == "warning on")
        {
            warningSystemScript.OpenWarning();
        }
        else if (args.text == "warning off") 
        {
            warningSystemScript.CloseWarning();
        }
        else if (args.text == "to do on")
        {
            warningSystemScript.OpenToDo();
        }
        else if (args.text == "to do off")
        {
            warningSystemScript.CloseToDo();
        }
        else if (args.text == "task done")
        {
            // noop
        }

        // telemetry
        else if (args.text == "telemetry on")
        {
            telemetryScript.OpenTelemetry();
        }
        else if (args.text == "telemetry off")
        {
            telemetryScript.CloseTime();
        }
        else if (args.text == "page one")
        {
            telemetryScript.PageOne();
        }
        else if (args.text == "page two")
        {
            telemetryScript.PageTwo();
        }
        else if (args.text == "time on")
        {
            telemetryScript.OpenTime();
        }
        else if (args.text == "time off")
        {
            telemetryScript.CloseTime();
        }

        // critical telemetry
        else if (args.text == "critical on")
        {

        }
        else if (args.text == "critical off")
        {

        }
        else if (args.text == "bpm on")
        {

        }
        else if (args.text == "bpm off")
        {

        }
        else if (args.text == "battery life on")
        {

        }
        else if (args.text == "battery life off")
        {

        }
        else if (args.text == "oxygen life on")
        {

        }
        else if (args.text == "oxygen life off")
        {

        }

        // compass
        else if (args.text == "compass on")
        {
            compassCanvas.gameObject.SetActive(true);
        }
        else if (args.text == "compass off")
        {
            compassCanvas.gameObject.SetActive(false);
        }

        // geo sampling
        else if (args.text == "geo on")
        {
            geoCanvas.gameObject.SetActive(true);
        }
        else if (args.text == "geo off")
        {
            geoCanvas.gameObject.SetActive(false);
        }
    }
}
