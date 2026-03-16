using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; private set; }

    public float givenTime;
    public Text timer;
    public GameObject buttonToStartTimer;   // In the start of each level there is panel appear to give the info about controlls, by pressing the button of Info panel the timer will start and the panel will disappear.
    public bool isTimerStarted { get; set; }

    private float timeLeft;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        timeLeft = givenTime;
        timer.gameObject.SetActive(false);
        isTimerStarted = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (TaptoStart.instance.isGameStart)
        {
            timer.gameObject.SetActive(true);

            if (isTimerStarted)
            {
                timeLeft -= Time.deltaTime;
            }

            if (timeLeft <= 0)
            {
                GameManager.Instance.LoseGame();
                CollisionDetection.Instance.DisableAllScripts();
            }else if (timeLeft > 60){
                FormatToMinSec();
            }else{
                timer.text = timeLeft.ToString("Timer Left: " + "0:00");
            }
        }
    }


    private void FormatToMinSec()
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        timer.text = string.Format("Timer Left: " + "{0:00}:{1:00}", minutes, seconds);
    }
}
