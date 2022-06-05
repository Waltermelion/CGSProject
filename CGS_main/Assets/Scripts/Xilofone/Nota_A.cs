using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota_A : MonoBehaviour
{ 
    
         public AudioSource A_Note;
    private void OnMouseDown()
    {
        {
            A_Note.Play();
        }

    }

}
