using UnityEngine;

public class SwipeController : MonoBehaviour
{

    // Singleton Instance
    public static SwipeController Instance;

    // Public Fields
    public float swipeThreshold = 20f;
    public float disBetweenLines = 3;
    public int currentLine = 1;
    
    // Private Fields
    private bool ispressing;
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
                Vector2 delta = pointEndPosition - pointStartPosition;


                // get the position of finger on the x-axis of the screen
                Vector2 deltaXstart = new Vector2(pointStartPosition.x, 0);
                Vector2 deltaXend = new Vector2(pointEndPosition.x, 0);


                // get the x-axis delta Length difference of start and end position of screen pointer
                float deltaLength = (deltaXend - deltaXstart).magnitude;

                Debug.Log("CurrentLine: update " + currentLine);

                // check if the delta length is greater then swipeThreshold and finger is moving on screen in x-axis direction
                if (delta.x > 0 && deltaLength > swipeThreshold && currentLine < 2)
                {

                    // set the active Object position to the right by line distance
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x + disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);

                    // increase the currentLine by 1
                    currentLine += 1;
                    ispressing = true;

                    Debug.Log("CurrentLine Right: " + currentLine);
                }

                // check if the delta length is greater then swipeThreshold and finger is moving on screen in -ve x-axis direction
                else if (delta.x < 0 && deltaLength > swipeThreshold && currentLine > 0)
                {
                    // set the active Object position to the left by line distance
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x - disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);

                    // decrease the currentLine by 1
                    currentLine -= 1;
                    ispressing = true;

                    Debug.Log("CurrentLine LEFT: " + currentLine);
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

}
