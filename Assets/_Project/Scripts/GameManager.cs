using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        playerMovement.speed = 0;
    }

    public void LoseGame()
    {
        playerMovement.speed = 0;
        losePanel.SetActive(true);
        losePanelAnimator.Play("LosePanelFadeIn");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MissionSelection");
    }
    



}
