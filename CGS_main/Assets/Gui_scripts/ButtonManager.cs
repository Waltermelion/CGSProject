using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //Button Puzzle
    public bool puzzleStart;
    private bool puzzleDone;
    public int ammountBttn;
    public GameObject[] buttonsClicked;
    public GameObject pilarButoes;
    public Animator doorOpen;
    
    //Timer
    private float seconds = 50;
    private bool timeSeconds = false;

    [SerializeField] private AudioSource timerS;
    [SerializeField] private AudioSource winS;
    [SerializeField] private AudioSource loseS;
    [SerializeField] private AudioSource ambient;

    private void Start()
    {
        ammountBttn = 10;
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
            timerS.Stop();
            winS.Play();
            loseS.Stop();
            pilarButoes.SetActive(false);
            ambient.Play();
            puzzleDone = true;
            doorOpen.SetTrigger("AbrirTres");
            Debug.Log("Puzzle Done");
        }
        
        //Timer
        if (puzzleStart)
        {
            if (timeSeconds == false && seconds > 0)
            {
                StartCoroutine(CounterSecond());
            }

            if (seconds <= 0)
            {
                pilarButoes.SetActive(false);
                loseS.Play();
                ambient.Play();
                puzzleStart = false;
                seconds = 50;
            }
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
