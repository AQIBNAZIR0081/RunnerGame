using UnityEngine;
using UnityEngine.UI;

public class CoinUIController : MonoBehaviour
{
    public Text coinCountText;

    CoinManager coinManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinManager = GameObject.FindAnyObjectByType<CoinManager>().GetComponent<CoinManager>();
    }

    // Update is called once per frame
    public void UpdateScoreOnUI()
    {
           coinCountText.text = coinManager.score.ToString();
    }
}
