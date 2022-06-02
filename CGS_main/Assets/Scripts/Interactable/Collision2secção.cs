using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2secção : MonoBehaviour
{
    [Header("SetActive")]
    public GameObject OAzul;
    public GameObject OAmarelo;
    public GameObject OVerde;
    public GameObject OVermelho;
    public GameObject ORosa;

    [Header("Destroy")]
    public GameObject AzulTapete;
    public GameObject AmareloTapete;
    public GameObject VerdeTapete;
    public GameObject VermelhoTapete;
    public GameObject RosaTapete;

     public bool isActive = false;

    private void Update()
    {           
        
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "AzulTapete")
        {
            OAzul.SetActive(false);
            AzulTapete.SetActive(false);
            isActive = true;              
        }

        if (collision.gameObject.name == "VerdeTapete")
        {
            OAmarelo.SetActive(false);
            VerdeTapete.SetActive(false);
            isActive = true;          
        }

        if (collision.gameObject.name == "VermelhoTapete")
        {
            OVerde.SetActive(false);
            VermelhoTapete.SetActive(false);
            isActive = true;           
        }

        if (collision.gameObject.name == "RosaTapete")
        {
            OVermelho.SetActive(false);
            RosaTapete.SetActive(false);
            isActive = true;             
        }

        if (collision.gameObject.name == "AmareloTapete")
        {
            ORosa.SetActive(false);
            AmareloTapete.SetActive(false);
            isActive = true;       
        }
    }
}
