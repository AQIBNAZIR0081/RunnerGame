using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance;

    public float swipeThreshold = 20f;
    public int disBetweenLines = 3;
    public int currentLine = 1;
    
    private bool ispressing;
    private Vector2 pointStartPosition, pointEndPosition;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void TouchesInput(GameObject activeObject)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                pointStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved && ispressing == false)
            {
                Vector2 delta = pointEndPosition - pointStartPosition;

                Vector2 deltaXstart = new Vector2(pointStartPosition.x, 0);
                Vector2 deltaXend = new Vector2(pointEndPosition.x, 0);

                float deltaLength = (deltaXend - deltaXstart).magnitude;

                Debug.Log("CurrentLine: update " + currentLine);

                // move right
                if (delta.x > 0 && deltaLength > swipeThreshold && currentLine < 2)
                {
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x + disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);
                    currentLine += 1;
                    ispressing = true;
                    Debug.Log("CurrentLine Right: " + currentLine);
                }

                else if(delta.x < 0 && deltaLength > swipeThreshold && currentLine > 0)
                {
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x - disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);
                    currentLine -= 1;
                    ispressing = true;
                    Debug.Log("CurrentLine LEFT: " + currentLine);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                pointEndPosition = touch.position;
                ispressing = false;
            }
        }
    }

}
