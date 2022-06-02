using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Destroy")]
    public GameObject Dust;
    public GameObject Azul;
    public GameObject Amarelo;
    public GameObject Verde;
    public GameObject Vermelho;
    public GameObject Rosa;

    [Header("SetActive")]
    public GameObject OAzul;
    public GameObject OAmarelo;
    public GameObject OVerde;
    public GameObject OVermelho;
    public GameObject ORosa;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Drop X here")
        {
            Dust.SetActive(false);
        }

        if (collision.gameObject.name == "Secret Azul")
        {
            Azul.SetActive(false);
            OAzul.SetActive(true);
        }

        if (collision.gameObject.name == "Secret Vermelho")
        {
            Vermelho.SetActive(false);
            OVermelho.SetActive(true);
        }

        if (collision.gameObject.name == "Secret Amarelo")
        {
            Amarelo.SetActive(false);
            OAmarelo.SetActive(true);
        }

        if (collision.gameObject.name == "Secret Verde")
        {
            Verde.SetActive(false);
            OVerde.SetActive(true);
        }

        if (collision.gameObject.name == "Secret Rosa")
        {
            Rosa.SetActive(false);
            ORosa.SetActive(true);
        }
    }
}
