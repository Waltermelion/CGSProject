using UnityEngine;

public class ButtonClicker : MonoBehaviour
{
    public Animator bttn;
    public InputManager ipt;

    public float dist = 5f;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "Button")
        {
            if (ipt.onFoot.Interact.triggered)
            {
                Debug.Log("you are looking at the button");
                bttn.SetTrigger("BotaoCarregado");
            }
        }
    }
}
