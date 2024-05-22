using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandMenuController : MonoBehaviour // basic scene managing script
{

    

    [SerializeField] ConnectionHandler connectionHandler;

    [Header("IP Config")]
    [SerializeField] TMP_Text setIPText;
    [SerializeField] TMP_Text currentIPText;

    [Header("Set Map Pos")]
    [SerializeField] GameObject cameraOffset;
    [SerializeField] TMP_Text xPosText;
    [SerializeField] TMP_Text zPosText;
    TMP_Text currentPosText;

    [Header("Toggle Telemtry Display")]
    [SerializeField] GameObject telemetryCanvas;

    [Header("Toggle Geosammple Display")]
    [SerializeField] GameObject geosample;

    [Header("Toggle Flare Detection")]
    [SerializeField] GameObject flareCanvas;

    // Start is called before the first frame update
    void Start()
    {
        currentPosText = xPosText;
    }

    // Update is called once per frame
    void Update()
    {
        currentIPText.text = "Current IP:\n" + PlayerPrefs.GetString("CurrentIP"); // default: 0.0.0.0
    }

    public void QuitApplication() // quit app button action
    {
        Application.Quit();
    }

    public void SetIP() // add ip button action
    {
        SetIP(setIPText.text);
    }

    public void SetIP(string ip) // add ip button action
    {
        if (ip == "Type an IP" || ip.Length < 7) return;
        PlayerPrefs.SetString("CurrentIP", ip);
        connectionHandler.SetHostIP(PlayerPrefs.GetString("CurrentIP"));
        setIPText.text = "Type an IP";
    }

    public void NumpadInputIP(string input) // numpad button action
    {
        if (setIPText.text == "Type an IP") setIPText.text = "";
        if (input == "backspace") { if (setIPText.text.Length > 0) setIPText.text = setIPText.text.Substring(0, setIPText.text.Length - 1); }
        else setIPText.text += input;
        if (setIPText.text.Length == 0) setIPText.text = "Type an IP";
    }

    public void SetCoords()
    {
        SetCoords(xPosText.text, zPosText.text);
    }
    public void SetCoords(string x, string z)
    {
        if (x == "N/S Coords" || z == "E/W Coords") return;
        xPosText.text = "E/W Coords";
        zPosText.text = "N/S Coords";
        try
        {
            float xPos = float.Parse(x);
            float zPos = float.Parse(z);
            SetUserPosition(new Vector3(xPos,0,zPos));
        }
        catch { }
    }

    public void NumpadInputMap(string input) // numpad button action
    {
        if (currentPosText.text == "N/S Coords" || currentPosText.text == "E/W Coords") currentPosText.text = "";
        if (input == "backspace") { if (currentPosText.text.Length > 0) currentPosText.text = currentPosText.text.Substring(0, currentPosText.text.Length - 1); }
        else currentPosText.text += input;
        if (currentPosText.text.Length == 0)
        {
            if (currentPosText == xPosText) currentPosText.text = "E/W Coords";
            else if (currentPosText == zPosText) currentPosText.text = "N/S Coords";
        }
    }

    public void SelectCoordsBox(int num) { if (num == 0) currentPosText = xPosText; else if (num == 1) currentPosText = zPosText; }

    public void SetUserPosition()
    {
        SetUserPosition(new Vector3(0,0,0));
    }

    public void SetUserPosition(Vector3 newPos)
    {
        Vector3 currentCameraPos = Camera.main.transform.position;
        float newX = newPos.x - currentCameraPos.x + cameraOffset.transform.position.x;
        float newZ = newPos.z - currentCameraPos.z + cameraOffset.transform.position.z;
        cameraOffset.transform.position = new Vector3(newX, cameraOffset.transform.position.y, newZ);
    }

    public void ToggleTelemetry()
    {
        telemetryCanvas.SetActive(!telemetryCanvas.activeSelf);
    }
    public void ToggleGeosample()
    {
        geosample.SetActive(!geosample.activeSelf);
    }

    public void ToggleFlare()
    {
        flareCanvas.SetActive(!flareCanvas.activeSelf);
    }
}
