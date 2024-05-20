using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EVAController : MonoBehaviour
{
    [SerializeField] TMP_Text switchEVAText;
    [Header("Serialized for Debug")]
    [SerializeField] int evaNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("evaNumber") != 1 || PlayerPrefs.GetInt("evaNumber") != 2) PlayerPrefs.SetInt("evaNumber", 1);
        evaNumber = PlayerPrefs.GetInt("evaNumber");
        switchEVAText.text = "Switch EVA\n(Current: " + evaNumber + ")";
    }

    // Update is called once per frame
    void Update()
    {

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
}
