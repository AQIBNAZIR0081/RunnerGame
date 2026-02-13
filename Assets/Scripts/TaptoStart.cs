using UnityEngine;

public class TaptoStart : MonoBehaviour
{
    public static TaptoStart instance;
    public bool isGameStart;

    public GameObject tapToStartBtn;
    private Animator animator;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isGameStart = false;
    }

    public void GameStarted()
    {
        isGameStart = true;
        tapToStartBtn.SetActive(false);
        
    }
}
