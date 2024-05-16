using System;
using System.Collections.Generic;
using UnityEngine;


public class ConnectionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GatewayConnection GatewayConnection;

    void Start()
    {
        if (PlayerPrefs.GetString("CurrentIP") == null) PlayerPrefs.SetString("CurrentIP", "172.31.233.148");
        GatewayConnection.ConnectToHost(PlayerPrefs.GetString("CurrentIP"), 3001);
    }

    public GatewayConnection GetConnection()
    {
        return this.GatewayConnection;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHostIP(string ip)
    {
        GatewayConnection.DisconnectFromHost();
        GatewayConnection.ConnectToHost(ip, 3001);
    }
}
