using UnityEngine;

public class EggButtonClick : MonoBehaviour
{
    public GameObject bttnmgr;
    public AudioSource audioSource3;
    public AudioClip eggDropSound;
    public AudioClip buttonClip;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        audioSource3.PlayOneShot(eggDropSound);
        if (collision.collider.tag == "Button")
        {
            audioSource3.PlayOneShot(buttonClip);
            collision.collider.GetComponent<Animator>().SetTrigger("BotaoCarregado");
            collision.collider.GetComponent<ButtonInd>().iHaveBeenClicked = true;
            if (!bttnmgr.GetComponent<ButtonManager>().puzzleStart)
            {
                bttnmgr.GetComponent<ButtonManager>().puzzleStart = true;
            }
        }
    }
}
