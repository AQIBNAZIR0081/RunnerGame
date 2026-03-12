using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public static CollisionDetection Instance { get; set; }
    public AudioSource hitSound;
    public Slider healthSlider;
    public float healthReductionAmount;
    public float healthIncreasingAmount;

    private float currentHealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Initialize health slider to full health
        healthSlider.value = 1.0f;
        currentHealth = healthSlider.value;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
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
                    if (particle != null)
                    {
                        particle.Play();
                    }
                    DeactivateObjectOnCollision(collision.gameObject);
                    break;
            }
        }

        if (collision.gameObject.CompareTag("Bridge"))
        {
            HandleHit();
        }
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

    private void DeactivateObjectOnCollision(GameObject obj)
    {
        AudioSource collisionObjAudio = obj.GetComponent<AudioSource>();
        if (collisionObjAudio != null && !collisionObjAudio.isPlaying)
        {
            collisionObjAudio.Play();
            StartCoroutine(DisableObject(obj));
        }
    }

    IEnumerator DisableObject(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
    }

    public void DisableAllScripts()
    {
        MonoBehaviour[] attachedScripts = gameObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour attachScript in attachedScripts)
        {
            attachScript.enabled = false;
        }

        Animator anim = gameObject.GetComponent<Animator>();        
        if(anim != null)
        {
            anim.enabled = false;
        }
        gameObject.GetComponent<AudioSource>().enabled = false;

        Debug.Log("All scripts disabled.");
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
