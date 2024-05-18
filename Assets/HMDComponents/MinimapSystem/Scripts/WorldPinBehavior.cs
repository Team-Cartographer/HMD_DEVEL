using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldPinBehavior : MonoBehaviour
{
    GameObject mainCamera;
    [SerializeField] float distanceMetersShowPopup = 2;

    [Header("For Removable Pins")]
    [SerializeField] bool makeRemovable = true;
    [SerializeField] GameObject popup;
    [SerializeField] TMP_Text nameText;
    string pinName;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!makeRemovable) return;

        if (Mathf.Abs((transform.position - mainCamera.transform.position).magnitude) <= distanceMetersShowPopup)
        {
            popup.SetActive(true);
            popup.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
        else popup.SetActive(false);
    }

    public void RemoveThisPin()
    {
        Debug.Log("attempting to delete pin");
        FindAnyObjectByType<HMDPinsSync>().RemovePin(transform.position, pinName);
    }

    public void SetPinName(string n) 
    {
        pinName = n;
        if (pinName == "" || pinName.Length == 0 || pinName == null) nameText.text = "Unnamed Pin";
        else nameText.text = "Name:\n" + n; 
    }
}
