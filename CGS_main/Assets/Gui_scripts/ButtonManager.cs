using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //Button Puzzle
    private bool puzzleDone;
    public int ammountBttn;
    public GameObject[] buttonsClicked;
    public Animator doorOpen;
    
    //Timer
    public GameObject countDown;
    private float seconds = 30;
    private bool timeSeconds = false;

    public GameObject Zero;
    
    private void Start()
    {
        ammountBttn = 10;
        
        Zero.SetActive(false);
    }
    private void Update()
    {
        //Puzzle
        if (!puzzleDone)
        {
            foreach (GameObject gm in buttonsClicked)
            {
                if (ammountBttn != 0)
                {
                    if (gm.GetComponent<ButtonInd>().iHaveBeenClicked && !gm.GetComponent<ButtonInd>().iHaveBeenChecked)
                    {
                        ammountBttn--;
                        gm.GetComponent<ButtonInd>().iHaveBeenChecked = true;
                    }
                }
            }  
        }

        if (ammountBttn == 0 && !puzzleDone)
        {
            puzzleDone = true;
            doorOpen.SetTrigger("AbrirTres");
            Debug.Log("Puzzle Done");
        }
        
        //Timer
        if (timeSeconds == false && seconds > 0)
        {
            StartCoroutine(CounterSecond());
        }

        if (seconds <= 0)
        {
                countDown.SetActive(false);
        }
    }

    IEnumerator CounterSecond()
    {
        timeSeconds = true;
        yield return new WaitForSeconds(1);
        seconds -= 1;
        timeSeconds = false;
    }
}
