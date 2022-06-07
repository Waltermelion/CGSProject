using UnityEngine;

public class ButtonClicker : MonoBehaviour
{
    public InputManager ipt;

    public float dist = 5f;

    public GameObject bttnmgr;
    
    //Audio
    public AudioSource audioSource3;
    public AudioClip buttonClip;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "Button")
        {
            if (ipt.onFoot.Interact.triggered)
            {
                audioSource3.PlayOneShot(buttonClip);
                Debug.Log("you are looking at the button");
                hit.collider.GetComponent<Animator>().SetTrigger("BotaoCarregado");
                hit.collider.GetComponent<ButtonInd>().iHaveBeenClicked = true;
                if (!bttnmgr.GetComponent<ButtonManager>().puzzleStart)
                {
                    bttnmgr.GetComponent<ButtonManager>().puzzleStart = true;
                }
            }
        }
    }
}
