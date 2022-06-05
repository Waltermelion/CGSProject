using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EggManager : Collision2secção
{
    [SerializeField]
    public GameObject[] eggs;

    [Header("Botao")]
    public GameObject botao;

    private bool paredeCai;

    int counter = 0;

    private void Start()
    {
        botao.SetActive(false);
    }
    private void Update()
    {
        if (paredeCai == false)
        {
            for (int i = 0; i < eggs.Length; i++)
            {
                if (eggs[i].GetComponent<Collision2secção>().isActive)
                {
                    counter++;
                }
            }

            if (counter == eggs.Length)
            {
                botao.SetActive(true);
            }

            counter = 0;
        }
    }
}
