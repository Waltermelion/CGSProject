using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    private bool fadeOut;
    private bool faded;

    public Image cubeblack;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGame")
            fadeOut = true;
    }

    private void Update()
    {
        if (fadeOut)
        {
            var tempColor = cubeblack.color;
            tempColor.a += 1f;
            cubeblack.color = tempColor;
        }else if (cubeblack.color.a >= 220f)
        {
            faded = true;
        }

        if (faded)
            SceneManager.LoadScene("EndingCredits");
    }
}