using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    // Singleton Instance
    public static SwipeController Instance;

    // Public Fields
    public float swipeThreshold = 20f;
    public float disBetweenLines = 3;
    public float duration = 0.2f;
    public int currentLine = 1;

    // Private Fields
    private GameObject refObj;
    private bool ispressing;
    private bool isInterpolate = false;
    private float elapsedTime;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector2 pointStartPosition, pointEndPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (isInterpolate)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);
            float newXPosition = Mathf.Lerp(initialPosition.x, targetPosition.x, t);

            Vector3 currentPos = refObj.transform.position;

            refObj.transform.position =
                new Vector3(newXPosition, currentPos.y, currentPos.z);

            if (t >= 1f)
            {
                isInterpolate = false;
            }
        }
    }

    // TouchInput Method for each player GameObject present in game
    public void TouchesInput(GameObject activeObject)
    {
        refObj = activeObject;
        // If there is touch on the screen
        if (Input.touchCount > 0)
        {

            // get the first touch on the screen
            Touch touch = Input.GetTouch(0);

            // When finger is place at screen
            if (touch.phase == TouchPhase.Began)
            {
                pointStartPosition = touch.position;
            }

            // When finger in moving on the screen
            if (touch.phase == TouchPhase.Moved && !ispressing)
            {

                // get the finger first and last position difference
                Vector2 delta = touch.position - pointStartPosition;

                if (Mathf.Abs(delta.x) > swipeThreshold)
                {
                    // swipe Right
                    if (delta.x > 0 && currentLine < 2)
                    {
                        currentLine++;
                        InterpolateBetweenLines(false);
                    }
                    else if (delta.x < 0 && currentLine > 0)
                    {
                        currentLine--;
                        InterpolateBetweenLines(true);
                    }
                    ispressing = true;
                }
            }

            // set the position of finger pointer on the screen when finger is 
            if (touch.phase == TouchPhase.Ended)
            {
                pointEndPosition = touch.position;
                ispressing = false;
            }
        }
    }

    void InterpolateBetweenLines(bool rightToLeft)
    {
        if (isInterpolate) return;

        initialPosition =   new Vector3( refObj.transform.position.x, 0, 0);

        if (rightToLeft)
        {
            targetPosition = new Vector3(initialPosition.x - disBetweenLines, initialPosition.y, initialPosition.z);
        }
        else
        {
            targetPosition = new Vector3(initialPosition.x + disBetweenLines, initialPosition.y, initialPosition.z);
        }

        elapsedTime = 0f;
        isInterpolate = true;
    }

}