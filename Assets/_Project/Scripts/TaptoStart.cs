using UnityEngine;

public class TaptoStart : MonoBehaviour
{
    public static TaptoStart instance;
    
    public GameObject InfoCanvas;
    public bool isGameStart { get; set; }

    private Animator animator;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isGameStart = false;
    }

    public void GameStarted()
    {
        isGameStart = true;
        InfoCanvas.SetActive(false);
    }
}
