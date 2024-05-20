using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.XR.LegacyInputHelpers;


public class TelemetryScript : MonoBehaviour
{
    public TMP_Text telemetryHeader;
    public TMP_Text telemetryBody;
    public Canvas telemetryCanvas;
    public TMP_Text activeTime;


    public ConnectionHandler connectionHandler;

    public int time;
    public JObject eva1;
    public JObject eva2;


    public static string FormatTime(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int secs = seconds % 60;

        return $"{hours:D2}:{minutes:D2}:{secs:D2}";
    }

    public static string ConvertToString(JObject jObj)
    {
        if (jObj == null)
        {
            return "Null jObj was found";
        }
        string result = "";

        foreach (var pair in jObj)
        {
            result += $"{pair.Key} : {pair.Value}\n";
        }

        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        telemetryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // Networking
        GatewayConnection gatewayConnection = connectionHandler.GetConnection();

        if (gatewayConnection != null && gatewayConnection.isTELEMETRYUpdated())
        {
            Debug.Log(gatewayConnection.GetTELEMETRYJsonString());
            JObject telemetryTotal = JObject.Parse(gatewayConnection.GetTELEMETRYJsonString());
            JToken telemetry = telemetryTotal["telemetry"];
            time = telemetry["eva_time"].ToObject<int>();

            eva1 = telemetry["eva1"].ToObject<JObject>();
            eva2 = telemetry["eva2"].ToObject<JObject>();

            activeTime.text = FormatTime(time);
        }


        // displaying
        /*
        if (Input.GetKeyDown(KeyCode.X))  // Change to hand commands
        {
            telemetryCanvas.gameObject.SetActive(!telemetryCanvas.gameObject.activeSelf);
        }
        if (telemetryCanvas.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))  // Change to hand commands
            {
                PageOne();

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))  // Change to hand commands
            {
                PageTwo();

            }

            if (telemetryHeader.text == "EVA 2 Telemetry Data:")
            {
                telemetryBody.text = ConvertToString(eva2);
            }
            else
            {
                telemetryBody.text = ConvertToString(eva1);
            }

        }*/
    }

    public void OpenTelemetry()
    {
        telemetryCanvas.gameObject.SetActive(true);
    }
    public void CloseTelemetry()
    {
        telemetryCanvas.gameObject.SetActive(false);
    }
    public void PageOne()
    {
        telemetryHeader.text = "EVA 1 Telemetry Data:";
    }
    public void PageTwo()
    {
        telemetryHeader.text = "EVA 2 Telemetry Data:";
    }
    public void OpenTime()
    {
        activeTime.gameObject.SetActive(true);
    }
    public void CloseTime()
    {
        activeTime.gameObject.SetActive(false);
    }
}
