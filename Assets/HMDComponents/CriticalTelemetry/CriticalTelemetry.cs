using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class CriticalTelemetry : MonoBehaviour
{
    public TMP_Text bpmText;
    public TMP_Text battLife;
    public TMP_Text oxyTime;

    public ConnectionHandler connection;


    // Start is called before the first frame update
    void Start()
    {
        bpmText.gameObject.SetActive(true);
        battLife.gameObject.SetActive(true);
        oxyTime.gameObject.SetActive(true);
    }

    // Update is called once per frame
    // void Update()
    // {
    // 
    //     /// CHECK/UPDATE TODOLIST 
    //     if (connection != null)
    //     {
    // 
    //         GatewayConnection conn = connection.GetConnection();
    // 
    //         if (conn != null)
    //         {
    //             string telem = conn.GetTELEMETRYJsonString();
    //             JObject jo = JObject.Parse(telem);
    // 
    //             bpmText.text = "BPM: ";
    // 
    //             float bpm1 = jo["telemetry"]["eva1"]["heart_rate"].ToObject<float>();
    //             bpmText.text += bpm1;
    // 
    //             if (jo["telemetry"]["eva1"]["heart_rate"].ToObject<float>() > 100) //change to off-nominal values
    //             {
    //                 bpmText.color = Color.red;
    //             }
    //             else
    //             {
    //                 bpmText.color = Color.green;
    //             }
    // 
    //             battLife.text = "Remaining Battery Life: ";
    //             float batlife = jo["telemetry"]["eva1"]["batt_time_left"].ToObject<float>();
    //             int bhours = (int)batlife / 3600;
    //             batlife %= 3600;
    //             int bminutes = (int)batlife / 60;
    //             batlife %= 60;
    // 
    //             battLife.text += $"{bhours}:{bminutes}:{(int)batlife}";
    // 
    //             oxyTime.text = "Remaining Oxygen Time: ";
    //             float oxytime = jo["telemetry"]["eva1"]["oxy_time_left"].ToObject<float>();
    // 
    //             int ohours = (int)oxytime / 3600;
    //             oxytime %= 3600;
    //             int ominutes = (int)oxytime / 60;
    //             oxytime %= 60;
    // 
    //             oxyTime.text += $"{ohours}:{ominutes}:{(int)oxytime}";
    //         }
    //     }
    // 
    // }

    void Update()
    {
        if (connection == null)
        {
            Debug.LogError("Connection handler is not assigned.");
            return;
        }

        GatewayConnection conn = connection.GetConnection();
        if (conn == null)
        {
            Debug.LogError("Failed to get a valid connection.");
            return;
        }

        string telem = conn.GetTELEMETRYJsonString();
        if (string.IsNullOrEmpty(telem))
        {
            Debug.LogError("Telemetry data is empty or null.");
            return;
        }

        try
        {
            JObject jo = JObject.Parse(telem);

            UpdateBPM(jo);
            UpdateBatteryLife(jo);
            UpdateOxygenTime(jo);
        }
        catch (JsonReaderException ex)
        {
            Debug.LogError($"Failed to parse JSON: {ex.Message}. Data: {telem}");
        }
    }

    void UpdateBPM(JObject jo)
    {
        bpmText.text = "BPM: ";
        float bpm1 = jo["telemetry"]["eva1"]["heart_rate"].ToObject<float>();
        bpmText.text += bpm1;

        bpmText.color = bpm1 > 100 ? Color.red : Color.green;
    }

    void UpdateBatteryLife(JObject jo)
    {
        battLife.text = "Remaining Battery Life: ";
        float batlife = jo["telemetry"]["eva1"]["batt_time_left"].ToObject<float>();
        int bhours = (int)batlife / 3600;
        batlife %= 3600;
        int bminutes = (int)batlife / 60;
        batlife %= 60;

        battLife.text += $"{bhours:D2}:{bminutes:D2}:{(int)batlife:D2}";
    }

    void UpdateOxygenTime(JObject jo)
    {
        oxyTime.text = "Remaining Oxygen Time: ";
        float oxytime = jo["telemetry"]["eva1"]["oxy_time_left"].ToObject<float>();

        int ohours = (int)oxytime / 3600;
        oxytime %= 3600;
        int ominutes = (int)oxytime / 60;
        oxytime %= 60;

        oxyTime.text += $"{ohours:D2}:{ominutes:D2}:{(int)oxytime:D2}";
    }

}
