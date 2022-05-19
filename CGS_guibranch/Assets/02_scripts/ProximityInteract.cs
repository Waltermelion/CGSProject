using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteract : MonoBehaviour
{
    private PlayerInput manager;
    [SerializeField] private GameObject holdLocation;
    private Vector3 initialLocation;

        private void Awake()
    {
        manager = GetComponent<PlayerInput>();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Outline>().enabled = true;
        initialLocation = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Outline>().enabled = false;
        other.transform.position = initialLocation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (manager.OnFoot.Fire.triggered)
        {
            other.transform.position = holdLocation.transform.position;
        }
    }
}
