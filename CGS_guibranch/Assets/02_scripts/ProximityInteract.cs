using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Outline>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Outline>().enabled = false;
    }
}
