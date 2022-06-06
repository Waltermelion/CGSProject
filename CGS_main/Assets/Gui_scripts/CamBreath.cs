using UnityEngine;

public class CamBreath : MonoBehaviour
{
    Vector3 startPos;
    public float amplitude = 0.1f;
    public float period = 2f;
    
    protected void Start() {
        startPos = transform.localPosition;
    }
    
    protected void Update() {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.localPosition = startPos + Vector3.up * distance;
    }
}