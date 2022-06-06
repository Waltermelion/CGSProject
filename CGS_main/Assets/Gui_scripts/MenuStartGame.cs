using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuStartGame : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}