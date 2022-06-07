using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGame")
        {
            SceneManager.LoadScene("EndingCredits");
        }
    }
}