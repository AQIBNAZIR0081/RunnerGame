using System;
using UnityEngine;

public class ClimbUp : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ClimbToTop"))
        {
            // Move the player up while they are in the trigger
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
        }
    }
}
