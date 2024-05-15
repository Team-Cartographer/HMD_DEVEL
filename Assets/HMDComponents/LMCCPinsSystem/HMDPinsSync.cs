using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System;

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
        StartCoroutine(AddHMDPinCourotine(ConvertUnityToUTMCoords(position)));
    }

    int[] ConvertUnityToUTMCoords(Vector3 position)
    {
        int xPos = (int)position.x + mapCenter[0];
        int yPos = (int)position.z + mapCenter[1];
        return new int[] { xPos, yPos };
    }

    IEnumerator AddHMDPinCourotine(int[] coords)
    {
        var data = new
        {
            type = "Feature",
            properties = new
            {
                name = "HMD_Pin",
                description = coords[0] + "x" + coords[1]
            },
            geometry = new
            {
                type = "Point",
                coordinates = new int[][] { coords }
            }
        };
        string jsonData = JsonConvert.SerializeObject(data);

        //string jsonData = "{\r\n  \"type\": \"Feature\",\r\n  \"properties\": {\r\n    \"name\": \"\",\r\n    \"description\": \"" + coords[0] + "x" + coords[1] + "\"\r\n  },\r\n  \"geometry\": {\r\n    \"type\": \"Point\",\r\n    \"coordinates\": [\r\n      [\r\n        "+ coords[0] + ",\r\n        " + coords[1] + "\r\n      ]\r\n    ]\r\n  }\r\n}";
        //string jsonData = "{\"type\": \"Feature\", \"properties\": {\"name\": \"\", \"description\": \"" + coords[0] + "x" + coords[1] + "\"}, \"geometry\": {\"type\": \"Point\",\"coordinates\": [[" + coords[0] + ", " + coords[1] + "]]}}";

        Debug.Log("Checking POST JSON:" + jsonData);

        UnityWebRequest request = new UnityWebRequest("http://192.168.1.18:3001/addfeature", "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}
