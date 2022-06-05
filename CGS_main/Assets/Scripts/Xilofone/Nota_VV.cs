using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota_VV : MonoBehaviour
{
    public AudioSource VV_Note;
    private void OnMouseDown()
    {
        {
           VV_Note.Play();
        }

    }
}
