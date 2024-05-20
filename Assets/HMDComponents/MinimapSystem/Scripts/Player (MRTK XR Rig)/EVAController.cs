using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class EVAController : MonoBehaviour
{
    [SerializeField] TMP_Text switchEVAText;
    [SerializeField] ConnectionHandler connectionHandler;
    GatewayConnection gatewayConnection;
    bool initializedPos;
    [SerializeField] LMCCPinsController pinsController;
    [SerializeField] HandMenuController handMenuController;
    [Header("Serialized for Debug")]
    [SerializeField] int evaNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        initializedPos = false;
        if (PlayerPrefs.GetInt("evaNumber") != 1 || PlayerPrefs.GetInt("evaNumber") != 2) PlayerPrefs.SetInt("evaNumber", 1);
        evaNumber = PlayerPrefs.GetInt("evaNumber");
        switchEVAText.text = "Switch EVA\n(Current: " + evaNumber + ")";
        gatewayConnection = connectionHandler.GetConnection();
        StartCoroutine(InitializePos());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!initializedPos && gatewayConnection != null && gatewayConnection.isIMUUpdated())
        {
            string jsonData = gatewayConnection.GetIMUJsonString();
            Debug.Log("imu: " + jsonData);
            JObject pinsJson = JObject.Parse(jsonData);
            JArray eva = (JArray)pinsJson["imu"]["eva" + evaNumber];
            double[] position = { (double)eva[1], (double)eva[0] }; // x, y
            double[] center = { pinsController.GetMapCenterUTM_yx()[1], pinsController.GetMapCenterUTM_yx()[0] }; // x, y
            handMenuController.SetUserPosition(new Vector3((float)(center[0] - position[0]), 0, (float)(center[1] - position[1])));
            initializedPos = true;
        }*/
    }

    public void PlacePin()
    {
        FindAnyObjectByType<HMDPinsSync>().AddHMDPin(Camera.main.transform.position);
    }
    public int GetEVANumber() {  return evaNumber; }
    public void SwitchEVANumber()
    {
        evaNumber = evaNumber == 1 ? 2 : evaNumber == 2 ? 1 : 1;
        PlayerPrefs.SetInt("evaNumber", evaNumber);
        switchEVAText.text = "Switch EVA\n(Current: " + evaNumber + ")";
    }

    IEnumerator InitializePos()
    {
        yield return new WaitForSecondsRealtime(5);
        string jsonData = gatewayConnection.GetIMUJsonString();
        Debug.Log("imu: " + jsonData);
        JObject pinsJson = JObject.Parse(jsonData);
        double xPos = (double)pinsJson["imu"]["eva"+evaNumber]["posy"];
        double yPos = (double)pinsJson["imu"]["eva"+evaNumber]["posx"];
        double[] center = { pinsController.GetMapCenterUTM_yx()[1], pinsController.GetMapCenterUTM_yx()[0] }; // x, y
        handMenuController.SetUserPosition(new Vector3((float)(center[0] - xPos), 0, (float)(center[1] - yPos)));
    }
}
