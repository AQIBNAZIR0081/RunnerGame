using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public AudioSource hitSound;
    public Slider healthSlider;
    public float healthReductionAmount;
    public float healthIncreasingAmount;

    [SerializeField]
    private float currentHealth;

    private void Start()
    {
        // Initialize health slider to full health
        healthSlider.value = 1.0f;
        currentHealth = healthSlider.value;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            AudioSource source = other.gameObject.GetComponent<AudioSource>();
            AudioManager.Instance.PlaySound(source);

            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.WinGame();

            DisableAllScripts();
        }

        SizeReducer(other);
        
        SizeIncreaser(other);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            CharacterEnums charEnum = playerMovement.charEnums;
            
            switch (charEnum)
            {
                case CharacterEnums.Person:
                case CharacterEnums.Car:
                    HandleHit();
                    break;
                case CharacterEnums.Bulldozer:
                    ParticleSystem particle = collision.gameObject.GetComponentInChildren<ParticleSystem>();
                    Debug.Log("Particle system found: " + (particle != null));
                    if (particle != null)
                    {
                        particle.Play();
                    }
                    StartCoroutine(DeactivateObjectOnCollision(collision.gameObject));
                    break;
            }
        }
    }

    private void DisableAllScripts()
    {
        MonoBehaviour[] attachedScripts = gameObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour attachScript in attachedScripts)
        {
            attachScript.enabled = false;
        }

        gameObject.GetComponent<AudioSource>().enabled = false;
    }

    private void SizeIncreaser(Collider other)
    {
        // Increase the scale of the player when it enters a "SizeIncreasing" trigger
        if (other.gameObject.CompareTag("SizeIncreaser"))
        {

            if (currentHealth <= 1)
            {
                // Vibrate the device when the player trigger the SizeIncreasing trigger object.
                Handheld.Vibrate();

                // increase the health of the player when it exit a "SizeIncreasing" trigger
                IncreaseHealth(healthIncreasingAmount);

            }

            other.gameObject.SetActive(false);
        }
    }

    private void SizeReducer(Collider other)
    {
        // Decrease the scale of the player when it enters a "SizeReduction" trigger
        if (other.gameObject.CompareTag("SizeReducer"))
        {

            if (currentHealth > 0)
            {
                // Vibrate the device when the player trigger the SizeIncreasing trigger object.
                Handheld.Vibrate();

                // decrease the health of the player when it exit a "SizeReduction" trigger
                ReduceHealth(healthReductionAmount);

            }

            other.gameObject.SetActive(false);
        }

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

    private void HandleHit()
    {
        // Play the hit sound effect when the player collides with an obstacle
        hitSound.Play();
        // Vibrate the device when the player collides with an obstacle
        Handheld.Vibrate();

        currentHealth = 0f;
        healthSlider.value = currentHealth;
        
        DisableAllScripts();
        GameOver();
    }


    private void IncreaseHealth(float increaseAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + increaseAmount, 0, 1);
        healthSlider.value = currentHealth;
    }

    private void GameOver()
    {
        GameManager.Instance.LoseGame();
    }

    IEnumerator DeactivateObjectOnCollision(GameObject obj)
    {
        yield return new WaitForSeconds(0.02f);
        obj.SetActive(false);
    }

    #region LocalScaleChange
    /*
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
    */
    #endregion
}
