using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using JetBrains.Annotations;
using System.Linq;
using UnityEngine.XR.ARSubsystems;
using System.Net;
using System.Text;
using UnityEngine.InputSystem.XR;

public class LMCCPinsController : MonoBehaviour
{
    [SerializeField] ConnectionHandler connectionHandler;
    GatewayConnection gatewayConnection;
    HMDPinsSync hmdPinsSync;

    [SerializeField] GameObject lmccWorldPin;
    [SerializeField] GameObject lmccWorldBreadcrumb;
    [SerializeField] GameObject lmccRover;

    // serialized for debug
    [SerializeField] List<LMCCPin> lmccPins;
    [SerializeField] List<double> pinLatCoords;
    [SerializeField] List<double> pinLongCoords;
    [SerializeField] List<GameObject> worldLMCCPins;

    readonly int[] mapCenterUTM15 = { 1875, 1760 }; // x: 225 (left) - 3490 (right), y: 3290 (bottom) - 225 (top)
    readonly double[] mapCenterLatLon = { 29.564882056524166, -95.081497230282139 };
    readonly double[] latLongToMeter = { 110836.0, 97439.0 };

    // Start is called before the first frame update
    void Start()
    {
        gatewayConnection = connectionHandler.GetConnection();
        hmdPinsSync = GetComponent<HMDPinsSync>();
        hmdPinsSync.SetGatewayConnection(gatewayConnection);
        hmdPinsSync.SetMapCenter(mapCenterUTM15);
    }

    // Update is called once per frame
    void Update()
    {
        if (gatewayConnection != null && gatewayConnection.isGEOJSONHMDUpdated())
        {
            string jsonData = gatewayConnection.GetGEOJSONHMDJsonString();
            //Debug.Log(jsonData);
            UpdateLMCCPins(jsonData);
            //UpdatePinsOnField();
        }
    }



    void UpdatePinOnField(LMCCPin pin) // checks to see if any of the lmcc pins have a physical pin object in the world and creates one if not
    {
        /*foreach (LMCCPin pin in lmccPins)
        {
            
        }*/

        if (!pin.worldPin) // KEEP LOOKING HERE MAYBE FOR EVA/ROVER NOT SHOWING ON MAP
        {
            double[] convertedCoords = CenterUTMCoords(pin.coordinates);
            Vector3 worldPos = new Vector3((float)convertedCoords[0], 0, (float)convertedCoords[1]);
            //bool isPin = (name == "") || !(name == "EVA 1" || name == "EVA 2" || name == "Rover" || name == "EVA 1 Cache Point" || name == "EVA 2 Cache Point" || name == "Rover Cache Point");
            bool isBreadcrumb = pin.name == "EVA 1 Cache Point" || pin.name == "EVA 2 Cache Point" || pin.name == "Rover Cache Point";
            bool isEVA = pin.name == "EVA 1" || pin.name == "EVA 2";
            bool isRover = pin.name == "Rover";
            GameObject toInstantiate = isEVA ? lmccRover : isRover ? lmccRover : isBreadcrumb ? lmccWorldBreadcrumb : lmccWorldPin;
            pin.worldPin = Instantiate(toInstantiate, worldPos, Quaternion.identity);
        }
    }

    public double[] CenterUTMCoords(double[] lmccPinCoords)
    {
        double xPos = lmccPinCoords[0] - (double) mapCenterUTM15[0];
        double zPos = mapCenterUTM15[1] - (double) lmccPinCoords[1];
        double[] convertedPosition = { xPos, zPos };
        return convertedPosition;
    }

    void UpdateLMCCPins(string jsonString)
    {
        JObject pinsJson = JObject.Parse(jsonString);
        Debug.Log(jsonString);
        JArray features = (JArray) pinsJson["features"];
        List<double> retrievedLatCoords = new List<double>();
        List<double> retrievedLongCoords = new List<double>();
        foreach (JObject feature in features)
        {
            string name = (string)feature["name"];
            string description = (string)feature["description"];
            string[] coordinates = description.Split('x');
            double[] coords = { double.Parse(coordinates[0]), double.Parse(coordinates[1]) };
            retrievedLatCoords.Add(coords[0]);
            retrievedLongCoords.Add(coords[1]);
            if (!pinLatCoords.Contains(coords[0]) && !pinLongCoords.Contains(coords[1]))
            {
                pinLatCoords.Add(coords[0]);
                pinLongCoords.Add(coords[1]);
                LMCCPin newPin = new LMCCPin(name, coords);
                UpdatePinOnField(newPin);
                lmccPins.Add(newPin);
                
            }
        }
        Debug.Log("Size:" + lmccPins.Count());
        for (int i = lmccPins.Count() - 1; i >= 0; i--)
        { // KEEP LOOKING HERE MAYBE FOR EVA/ROVER NOT SHOWING ON MAP
            Debug.Log("name: " + lmccPins[i].name);
            bool containsCoords = retrievedLatCoords.Contains(lmccPins[i].coordinates[0]) &&
                retrievedLongCoords.Contains(lmccPins[i].coordinates[1]);
            bool isEVAorRover = lmccPins[i].name == "EVA 1" || lmccPins[i].name == "EVA 2" || lmccPins[i].name == "Rover";
            if (!isEVAorRover && !containsCoords)
            {
                if (lmccPins[i].worldPin) Destroy(lmccPins[i].worldPin);
                pinLatCoords.RemoveAt(i);
                pinLongCoords.RemoveAt(i);
                lmccPins.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class LMCCPin
{
    public string name;
    public double[] coordinates;
    public GameObject worldPin = null;
    public LMCCPin(string n, double[] c)
    {
        name = n;
        coordinates = c;
    }
}