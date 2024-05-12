using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class GeoSampleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text geoText;
    public ConnectionHandler connectionHandler;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GatewayConnection conn = connectionHandler.GetConnection();
        
        if (conn != null && conn.isSPECUpdated())
        {
            string spec = conn.GetSPECJsonString();
            JObject jo = JObject.Parse(spec);

            float id1 = jo["spec"]["eva1"]["id"].ToObject<float>();
            float id2 = jo["spec"]["eva2"]["id"].ToObject<float>();
            string text = "";
            if (id1 != 0)
            {
                text += "Eva1\n";
                text += $"ID: {id1}\n";

                if (jo != null && jo["spec"] != null && jo["spec"]["eva1"] != null && 
                    (jo["spec"]["eva1"]["data"]["Al2O3"].ToObject<float>() > 10 ||
                    jo["spec"]["eva1"]["data"]["SiO2"].ToObject<float>() < 10 ||
                    jo["spec"]["eva1"]["data"]["TiO2"].ToObject<float>() > 1 ||
                    jo["spec"]["eva1"]["data"]["FeO"].ToObject<float>() > 29 ||
                    jo["spec"]["eva1"]["data"]["MnO"].ToObject<float>() > 1 ||
                    jo["spec"]["eva1"]["data"]["MgO"].ToObject<float>() > 20 ||
                    jo["spec"]["eva1"]["data"]["CaO"].ToObject<float>() > 10 ||
                    jo["spec"]["eva1"]["data"]["K2O"].ToObject<float>() > 1 ||
                    jo["spec"]["eva1"]["data"]["P2O5"].ToObject<float>() > 1.5 ||
                    jo["spec"]["eva1"]["data"]["other"].ToObject<float>() > 50))
                {
                    text += "Sample Significant\n";
                    geoText.color = Color.green;
                } else
                {
                    text += "Sample insignificant\n";
                    geoText.color = Color.white;
                }

                foreach (var pair in jo["spec"]["eva1"]["data"].ToObject<JObject>())
                {
                    text += $"{pair.Key}: {pair.Value}\n";
                    //Debug.Log(pair.Key);
                }
                //geoText = $"eva1\n{}";
            }
            // if (id2 != 0)
            // {
            //     text += "Eva2\n";
            //     text += $"ID: {id2}\n";
            // 
            // 
            //     foreach (var pair in jo["spec"]["eva2"]["data"].ToObject<JObject>())
            //     {
            //         text += $"{pair.Key}: {pair.Value}\n";
            //         //Debug.Log(pair.Key);
            //     }
            //     //geoText = $"eva1\n{}";
            // }
            geoText.text = text;
        }
    }
}
