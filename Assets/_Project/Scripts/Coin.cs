using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public AudioClip coinCollectionSound;
    public float deactivateCoinDelay = 0.1f;
    public UnityEvent onCoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onCoinCollected?.Invoke();
            Invoke(nameof(DeactivateCoin), deactivateCoinDelay);
        }
    }

    public void PlayCoinCollectionSound()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (!source.isPlaying)
        {
            source.PlayOneShot(coinCollectionSound);
        }
    }

    private void DeactivateCoin()
    {
        gameObject.SetActive(false);
    }
}
