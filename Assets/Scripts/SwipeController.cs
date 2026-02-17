using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance;

    public float swipeThreshold = 20f;
    public int disBetweenLines = 3;
    public int currentLine = 1;
    public bool allowJumpToObject;
    
    
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

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = pointEndPosition - pointStartPosition;

                float deltaLength = (pointEndPosition - pointStartPosition).magnitude;


                // move right
                if(delta.x > 0 && currentLine < 2)
                {
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x + disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);
                    currentLine += 1;

                    Debug.Log("CurrentLine: " + currentLine);
                }

                else if(delta.x < 0 && currentLine > 0)
                {
                    activeObject.transform.position = new Vector3(activeObject.transform.position.x - disBetweenLines, activeObject.transform.position.y, activeObject.transform.position.z);
                    currentLine -= 1;

                    Debug.Log("CurrentLine: " + currentLine);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                pointEndPosition = touch.position;

                Vector2 pointerYend = new Vector2(0, pointEndPosition.y);
                Vector2 pointerYstart = new Vector2(0, pointStartPosition.y);

                float swipDiffVerticle = (pointerYend - pointerYstart).magnitude;

                if (pointEndPosition.y > pointStartPosition.y && swipDiffVerticle > swipeThreshold)
                {
                    allowJumpToObject = true;
                }

            }
        }
    }




}
