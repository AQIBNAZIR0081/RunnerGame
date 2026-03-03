using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public AudioClip coinCollectionSound;
    public UnityEvent onCoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onCoinCollected?.Invoke();
            Invoke(nameof(DeactivateCoin), 0.1f);
        }
    }

    public void PlayCoinCollectionSound()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.PlayOneShot(coinCollectionSound);
    }

    private void DeactivateCoin()
    {
        gameObject.SetActive(false);
    }
}
