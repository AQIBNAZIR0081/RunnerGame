using System;
using UnityEngine;

public class ClimbUp : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;

    private Animator anim;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ClimbToTop"))
        {
            // Move the player up while they are in the trigger
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
            anim = GetComponent<Animator>();

            if (anim != null)
            {
                anim.SetBool("IsClimbing", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ClimbToTop"))
        {
            // Stop climbing when the player exits the trigger
            anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("IsClimbing", false);
            }
        }
    }
}
