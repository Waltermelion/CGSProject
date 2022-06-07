using UnityEngine;
public class EndCreditsScroll : MonoBehaviour
{
    public float yPosOfCredits;
    void Update()
    {
        if (transform.position.y != yPosOfCredits)
        {
            var tempColor = transform.position;
            tempColor.y += 0.5f;
            transform.position = tempColor;
        }
    }
}