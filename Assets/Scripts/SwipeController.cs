using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject car;

    //public float swipeThreshold = 20f;
    public int disBetweenLines = 3;
    public int currentLine = 1;
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


                // move right
                if(delta.x > 0 && currentLine < 2)
                {
                    player.transform.position = new Vector3(player.transform.position.x + disBetweenLines, player.transform.position.y, player.transform.position.z);
                    currentLine++;

                    Debug.Log("CurrentLine: " + currentLine);
                }

                if(delta.x < 0 && currentLine > 0)
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
