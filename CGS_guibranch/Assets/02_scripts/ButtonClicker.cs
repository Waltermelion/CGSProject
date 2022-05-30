using UnityEngine;

public class ButtonClicker : MonoBehaviour
{
    public Animator bttn;
    public InputManager ipt;

    public float dist = 5f;

    private bool press;
    void Start()
    {
        
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward*2);
        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "Button")
        {
            if (ipt.onFoot.Fire.triggered)
            {
                Debug.Log("you are looking at the button");
                bttn.SetTrigger("Pressed");
            }            
        }
    }
}
