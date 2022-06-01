using System;
using UnityEngine;

public class ReverseGrav : MonoBehaviour
{
    public GameObject player;
    
    public float downScaler = 10f;

    private bool isReversed;

    private void FixedUpdate()
    {
        if (isReversed)
        {
            //player.GetComponent<Rigidbody>().AddForce(Vector3.down * downScaler);
        }
    }

    void reversegravity()
    {
        Debug.Log("REVERSE GRAVITY ACTIVATE");
        //player.GetComponent<Rigidbody>().useGravity = !player.GetComponent<Rigidbody>().useGravity;
        player.transform.Rotate(180f,0f,0f);
        isReversed = !isReversed;
        Physics.gravity = new Vector3(0f, 10f, 0f); //quase funciona, todos os cubos mudam a gravidade menos o jogador
    }
}
