using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int score {  get; private set; }

    public void IncrementScore()
    {
        score ++;
    }
}
