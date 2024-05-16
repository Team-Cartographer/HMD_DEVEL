using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPinBehavior : MonoBehaviour
{
    GameObject mainCamera;
    [SerializeField] float distanceMetersShowPopup = 2;
    [SerializeField] GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
        FindAnyObjectByType<HMDPinsSync>().RemovePin(transform.position);
    }
}
