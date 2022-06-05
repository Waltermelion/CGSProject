using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Sequece : MonoBehaviour
{
    public static event Action<string> SendColorValue = delegate { };

    public void ButtonClick()
    {
        SendColorValue(name.Substring(0, name.IndexOf("_")));
    }
}
