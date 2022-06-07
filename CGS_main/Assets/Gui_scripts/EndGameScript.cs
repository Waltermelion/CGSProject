using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGame") 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("EndingCredits");
    }
}