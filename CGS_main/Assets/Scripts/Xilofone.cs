using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xilofone : MonoBehaviour
{
    private string correctSequence, currentSequence;

    public GameObject xilofoneCanvas;
    public GameObject player;
    public AudioSource Do;
    public AudioSource Re;
    public AudioSource Mi;
    public AudioSource Fa;
    public AudioSource Sol;
    public AudioSource La;
    public AudioSource Si;
    public AudioSource DoA;

    public GameObject Acertou;

    // Start is called before the first frame update
    void Start()
    {
        Sequece.SendColorValue += AddValueAndCheckSequence;
        correctSequence = "12345678";
        currentSequence = "";

        Acertou.SetActive(false);
    }

    private void AddValueAndCheckSequence(string buttonColor)
    {

        switch (buttonColor)
        {
            case "Blue":
                currentSequence += 1;
                Sol.Play();
                break;
            case "Pink":
                currentSequence += 2;
                DoA.Play();
                break;
            case "Green":
                currentSequence += 3;
                Fa.Play();
                break;
            case "Violet":
                currentSequence += 4;
                Si.Play();
                break;
            case "Purple":
                currentSequence += 5;
                La.Play();
                break;
            case "Yellow":
                currentSequence += 6;
                Mi.Play();
                break;
            case "Orange":
                currentSequence += 7;
                Re.Play();
                break;
            case "Red":
                currentSequence += 8;
                Do.Play();
                break;
        }

        if (currentSequence != correctSequence.Substring(0, currentSequence.Length))
        {
            currentSequence = "";
        }
        else if (currentSequence == correctSequence)
        {
            currentSequence = "";
            Acertou.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Sequece.SendColorValue -= AddValueAndCheckSequence;
    }

}
