using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private bool puzzleDone;
    public int ammountBttn;
    public GameObject[] buttonsClicked;
    public Animator doorOpen;
    
    
    private void Start()
    {
        ammountBttn = 10;
    }
    private void Update()
    {
        if (!puzzleDone)
        {
            foreach (GameObject gm in buttonsClicked)
            {
                if (ammountBttn != 0)
                {
                    if (gm.GetComponent<ButtonInd>().iHaveBeenClicked && !gm.GetComponent<ButtonInd>().iHaveBeenChecked)
                    {
                        ammountBttn--;
                        gm.GetComponent<ButtonInd>().iHaveBeenChecked = true;
                    }
                }
            }  
        }

        if (ammountBttn == 0 && !puzzleDone)
        {
            puzzleDone = true;
            doorOpen.SetTrigger("AbrirTres");
            Debug.Log("Puzzle Done");
        }
    }
}
