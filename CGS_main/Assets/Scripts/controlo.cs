using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlo : MonoBehaviour
{
    public Animator anim;

   
    void Update()
    {
        anim.SetFloat("vertical",-1);
        anim.SetFloat("horizontal",1);
    }
}
