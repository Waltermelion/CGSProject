using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggButtonClick : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Button")
        {
            collision.collider.GetComponent<Animator>().SetTrigger("BotaoCarregado");
            collision.collider.GetComponent<ButtonInd>().iHaveBeenClicked = true;
        }
    }
}
