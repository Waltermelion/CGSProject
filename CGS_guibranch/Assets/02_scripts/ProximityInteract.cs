using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteract : MonoBehaviour
{
    private InputManager manager;
    [SerializeField] private GameObject holdLocation;
    private Vector3 initialLocation;
    private bool closeToObject = false;
    private bool pickedUpObject = false;
    private GameObject pickedObject;

    private void Awake()
    {
        manager = GetComponent<InputManager>();
    }
    private void Update()
    {
        if (manager.onFoot.Fire.triggered && closeToObject)
        {
            //pickedObject.transform.position = holdLocation.transform.position;
            pickedUpObject = !pickedUpObject;
            Debug.Log("im so triggered right now");
        }
        if (pickedUpObject)
        {
            pickedObject.transform.parent = holdLocation.transform;
            pickedObject.transform.localPosition = Vector3.zero;
            pickedObject.transform.localEulerAngles = Vector3.zero;
            pickedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pickedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else
        {
            if(pickedObject != null)
            {
                pickedObject.transform.parent = null;
            }            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Outline>().enabled = true;
        //initialLocation = other.transform.position;
        closeToObject = true;
    }   
    private void OnTriggerStay(Collider other)
    {
        if (!pickedUpObject)
        {
            pickedObject = other.gameObject;
        }
        closeToObject = true;
    }
    private void OnTriggerExit(Collider other)
    {       
        other.GetComponent<Outline>().enabled = false;
        //other.transform.position = initialLocation;
        closeToObject = false;
    }    
}
