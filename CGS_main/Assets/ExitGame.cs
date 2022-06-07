using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitGame;

    public void ExitGameClick()
    {
        Application.Quit();
    }
}
