using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public AudioSource coinSound;
    public UnityEvent onCoinCollected;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onCoinCollected?.Invoke();
            gameObject.SetActive(false);

        }
    }

    public void ManageAudio()
    {
        AudioSource source = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(source);
    }
}
