using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObjects : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("StopObject"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
