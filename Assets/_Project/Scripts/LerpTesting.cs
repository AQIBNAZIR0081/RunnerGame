using UnityEngine;

public class LerpTesting : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1f;

    private float startTime;
    private float journeyLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        Debug.Log("Start time:" + startTime);

        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        Debug.Log("Journey Length:" + journeyLength);

    }

    // Update is called once per frame
    void Update()
    {
        PerformLerp();
    }

    private void PerformLerp()
    {
        float disCovered = (Time.time - startTime) * speed;
        Debug.Log("Covered Distance:" + disCovered);

        float fractionJourney = disCovered / journeyLength;
        Debug.Log("Fraction Journey:" + fractionJourney);

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionJourney);
        Debug.Log("Current Position:" + transform.position);
    }
}
