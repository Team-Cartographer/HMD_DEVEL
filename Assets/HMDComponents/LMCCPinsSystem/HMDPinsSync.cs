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
        int[] coords = ConvertUnityToUTMCoords(position);
        var data = new
        {
            feature = new
            {
                type = "Feature",
                properties = new
                {
                    name = "",
                    description = coords[0] + "x" + coords[1]
                },
                geometry = new
                {
                    type = "Point",
                    coordinates = new int[][] { new int[] { coords[0], coords[1] } }
                }
            }
        };
        string jsonData = JsonConvert.SerializeObject(data);

        StartCoroutine(gatewayConnection.PostRequest("addfeature", jsonData));
    }

    public void RemovePin(Vector3 position)
    {
        int[] coords = ConvertUnityToUTMCoords(position);
        var data = new
        {
            feature = new
            {
                type = "Feature",
                properties = new
                {
                    name = "",
                    description = coords[0] + "x" + coords[1]
                },
                geometry = new
                {
                    type = "Point",
                    coordinates = new int[][] { new int[] { coords[0], coords[1] } }
                }
            }
        };
        string jsonData = JsonConvert.SerializeObject(data);

        StartCoroutine(gatewayConnection.PostRequest("removefeature", jsonData));
    }

    int[] ConvertUnityToUTMCoords(Vector3 position)
    {
        int xPos = mapCenter[0] + (int)position.x;
        int yPos = mapCenter[1] - (int)position.z;
        return new int[] { xPos, yPos };
    }
}
