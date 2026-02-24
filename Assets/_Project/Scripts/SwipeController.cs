using UnityEngine;

public class SwipeController : MonoBehaviour
{

    // Singleton Instance
    public static SwipeController Instance;

    // Public Fields
    public float swipeThreshold = 20f;
    public float disBetweenLines = 3;
    public float timeToInterpo = 0.2f;
    public int currentLine = 1;

    // Private Fields
    private bool ispressing;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector2 pointStartPosition, pointEndPosition;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // TouchInput Method for each player GameObject present in game
    public void TouchesInput(GameObject activeObject)
    {

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
            if (touch.phase == TouchPhase.Moved && ispressing == false)
            {

                // get the finger first and last position difference
                Vector2 delta = touch.position - pointStartPosition;


                // get the position of finger on the x-axis of the screen
                Vector2 deltaXstart = new Vector2(pointStartPosition.x, 0);
                Vector2 deltaXend = new Vector2(pointEndPosition.x, 0);


                // get the x-axis delta Length difference of start and end position of screen pointer
                float deltaLength = (deltaXend - deltaXstart).magnitude;


                // check if the delta length is greater then swipeThreshold and finger is moving on screen in x-axis direction
                if (delta.x > 0 && deltaLength > swipeThreshold && currentLine < 2)
                {

                    InterpolateBetweenLines(activeObject, false);

                    // increase the currentLine by 1
                    currentLine += 1;

                    ispressing = true;

                }

                // check if the delta length is greater then swipeThreshold and finger is moving on screen in -ve x-axis direction
                else if (delta.x < 0 && deltaLength > swipeThreshold && currentLine > 0)
                {
                    InterpolateBetweenLines(activeObject, true);

                    // decrease the currentLine by 1
                    currentLine -= 1;

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

    void InterpolateBetweenLines(GameObject obj, bool rightToLeft) 
    {
        initialPosition = obj.transform.position;
        Debug.Log("InitialPosition: " + initialPosition);


        if(rightToLeft== true)
        {
            targetPosition = new Vector3(initialPosition.x - disBetweenLines, initialPosition.y, initialPosition.z);
            Debug.Log("TargetPosition: "+ targetPosition);
        }
        else
        {
            targetPosition = new Vector3(initialPosition.x + disBetweenLines, initialPosition.y, initialPosition.z);
            Debug.Log("TargetPosition: " + targetPosition);
        }

        obj.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeToInterpo);

        //obj.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeToInterpo);
        Debug.Log("PositionAfterInterpolate " + obj.transform.position);

    }

}
