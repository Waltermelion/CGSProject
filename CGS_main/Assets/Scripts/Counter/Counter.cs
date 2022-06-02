using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public GameObject countDown;
    public TextMeshProUGUI counter;
    private int minutes = 14;
    private float seconds = 59;
    private bool timeSeconds = false;

    public GameObject Zero;
    public TextMeshProUGUI counterZero;

    private void Start()
    {
        counter.text = minutes.ToString() + ":" + seconds.ToString("F0");
        counterZero.text = "00:00";
        Zero.SetActive(false);
    }

    private void Update()
    {
        if (timeSeconds == false && seconds > 0)
        {
            StartCoroutine(CounterSecond());

            if (seconds <= 0)
            {
                seconds = 59;
                timeSeconds = false;
            }
        }

        if (minutes <= 0)
        {
            minutes = 0;

            if (seconds <= 0)
            {
                countDown.SetActive(false);
                Zero.SetActive(true);
            }
        }
    }

    IEnumerator CounterSecond()
    {
        timeSeconds = true;
        yield return new WaitForSeconds(1);
        seconds -= 1;
        counter.text = minutes.ToString() + ":" + seconds.ToString("F0");
        if (seconds <= 0)
        {
            minutes -= 1;
            seconds = 59;
        }
        timeSeconds = false;
    }
}
