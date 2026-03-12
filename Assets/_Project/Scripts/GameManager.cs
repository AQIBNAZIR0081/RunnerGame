using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject winPanel;
    public GameObject losePanel;
    public PlayerMovement playerMovement;

    private Animator losePanelAnimator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    private void Start()
    {
        losePanelAnimator = losePanel.GetComponent<Animator>();
    }


    public void WinGame()
    {
        winPanel.SetActive(true);
        TimerController.Instance.isTimerStarted = false;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        losePanelAnimator.Play("LosePanelFadeIn");
        TaptoStart.instance.isGameStart = false;
        TimerController.Instance.isTimerStarted = false;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("EnvironmentSelection");
    }
    

}
