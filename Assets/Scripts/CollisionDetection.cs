using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public AudioSource hitSound;
    public Slider healthSlider;
    public float scaleReductionAmount;
    public float scaleIncreasingAmount;

    [SerializeField]
    private float currentHealth;

    private void Start()
    {
        // Initialize health slider to full health
        healthSlider.value = 1.0f;

        currentHealth = healthSlider.value;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.WinGame();
        }


        // Decrease the scale of the player when it enters a "SizeReduction" trigger
        if (other.gameObject.CompareTag("SizeReducer"))
        {
           
            if (currentHealth > 0)
            {
                // Vibrate the device when the player trigger the SizeIncreasing trigger object.
                Handheld.Vibrate();

                // decrease the health of the player when it exit a "SizeReduction" trigger
                ReduceHealth(scaleReductionAmount);

            }

            other.gameObject.SetActive(false);
        }


        // Increase the scale of the player when it enters a "SizeIncreasing" trigger
        if (other.gameObject.CompareTag("SizeIncreaser"))
        {

            if (currentHealth <= 1)
            {
                // Vibrate the device when the player trigger the SizeIncreasing trigger object.
                Handheld.Vibrate();

                // increase the health of the player when it exit a "SizeIncreasing" trigger
                IncreaseHealth(scaleIncreasingAmount);
                
            }

            other.gameObject.SetActive(false);
        }

    }


    private void GameOver()
    {
        GameManager.Instance.LoseGame();
    }


    private void ReduceHealth(float decreaseAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - decreaseAmount, 0, 1);
        healthSlider.value = currentHealth;

        if(currentHealth <= 0.001)
        {
            Invoke(nameof(GameOver), 0.5f);
        }

    }

    private void IncreaseHealth(float increaseAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + increaseAmount, 0, 1);
        healthSlider.value = currentHealth;
    }

    private void ScaleReduction()
    {
        Vector3 currentScale = transform.localScale;

        float newXScale = currentScale.x - scaleReductionAmount;
        float newYScale = currentScale.y - scaleReductionAmount;
        float newZScale = currentScale.z - scaleReductionAmount;

        // Decrease the scale of the player by ScaleReductionAmount when it exit a "SizeReduction" trigger
        transform.localScale = new Vector3(newXScale, newYScale, newZScale);
    }

    private void ScaleIncreasing()
    {
        // store reference of original local scale
        Vector3 currentScale = transform.localScale;

        // reduce each axis scale by some amount
        float newXScale = currentScale.x + scaleIncreasingAmount;
        float newYScale = currentScale.y + scaleIncreasingAmount;
        float newZScale = currentScale.z + scaleIncreasingAmount;

        // Increase the scale of the player by ScaleInreasingAmount when it exit a "SizeIncreaser" trigger
        transform.localScale = new Vector3(newXScale, newYScale, newZScale);
    }
}
