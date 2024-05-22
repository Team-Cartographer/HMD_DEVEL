using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System;
using Newtonsoft.Json.Linq;

public class HMDPinsSync : MonoBehaviour
{
    GatewayConnection gatewayConnection;
    int[] mapCenter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGatewayConnection(GatewayConnection connection) { gatewayConnection = connection; }
    public void SetMapCenter(int[] center) { mapCenter = center; }

    public void AddHMDPin(Vector3 position)
    {
        Debug.Log("attempting to add pin");
        JArray geoJsonFeatures = (JArray)JObject.Parse(gatewayConnection.GetGEOJSONHMDJsonString())["features"]; 
        int evaNum = FindObjectOfType<EVAController>().GetEVANumber();

        // find the entry in the geoJson with the "properties"["name"] == "EVA 1"
        // then the description, UTM, and lat lon are all given to you, just add a new point at those given values
        // and its done 

        string desc = "0x0";
        double[] coords = { 0, 0 };
        double[] latloncoords = { 0, 0 };
        foreach (JObject jO in geoJsonFeatures)
        {
            if ((string)jO["name"] == "EVA " + evaNum)
            {
                desc = (string) jO["description"];
                JArray utm = (JArray)jO["utm"];
                coords = new double[] { (double)utm[0], (double)utm[1] };
                JArray latlon = (JArray)jO["latlon"];
                latloncoords = new double[] { (double)latlon[0], (double)latlon[1] };
                break;
            }
        }

        var data = new
        {
            feature = new
            {
                type = "Feature",
                geometry = new
                {
                    type = "Point",
                    coordinates = new double[]{ latloncoords[0], latloncoords[1] }
                },
                properties = new
                {
                    name = "HMDPoint",
                    description = desc,
                    utm = new double[] { coords[0], coords[1] }
                }
            }
        };
        string jsonData = JsonConvert.SerializeObject(data);

        StartCoroutine(gatewayConnection.PostRequest("addfeature", jsonData));
    }

    public void RemovePin(Vector3 position, string pinName)
    {
        /*int[] coords = ConvertUnityToUTMCoords(position);
        Debug.Log("attempting to remove pin");
        var data = new
        {
            feature = new
            {
                type = "Feature",
                geometry = new
                {
                    type = "Point",
                    coordinates = new int[] { coords[0], coords[1] }
                },
                properties = new
                {
                    name = pinName,
                    description = coords[0] + "x" + coords[1],
                    utm = new float[] { 0.0f, 0.0f }
                }
            }
        };
        string jsonData = JsonConvert.SerializeObject(data);

        StartCoroutine(gatewayConnection.PostRequest("removefeature", jsonData));*/
    }

    int[] ConvertUnityToUTMCoords(Vector3 position)
    {
        int xPos = mapCenter[0] + (int)position.x;
        int yPos = mapCenter[1] - (int)position.z;
        return new int[] { xPos, yPos };
    }
}
