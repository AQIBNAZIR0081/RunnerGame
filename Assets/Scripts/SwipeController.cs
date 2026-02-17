using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance;

    public GameObject player;
    public float swipeThreshold = 20f;
    public float disBetweenLines = 3f;
    public float currentLine = 1f;
    Vector2 pointStartPosition, pointEndPosition;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void TouchesInput()
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

                Debug.Log("Delta: " + delta.x);
                Debug.Log("Delta Length" + deltaLength);

                // move right
                if(delta.x > 0 && deltaLength > swipeThreshold && currentLine < 2)
                {
                    player.transform.position = new Vector3(player.transform.position.x + disBetweenLines, player.transform.position.y, player.transform.position.z);
                    currentLine++;

                    Debug.Log("CurrentLine: " + currentLine);
                }

                if(delta.x < 0 && deltaLength > swipeThreshold && currentLine > 0)
                {
                    player.transform.position = new Vector3(player.transform.position.x - disBetweenLines, player.transform.position.y, player.transform.position.z);
                    currentLine--;

                    Debug.Log("CurrentLine: " + currentLine);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                pointEndPosition = touch.position;
            }
        }
    }
}
