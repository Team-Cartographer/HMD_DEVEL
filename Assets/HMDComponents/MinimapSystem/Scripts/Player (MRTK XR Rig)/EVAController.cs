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
        switchEVAText.text = "Switch EVA\n(Current: " + evaNumber + ")";
    }
}
