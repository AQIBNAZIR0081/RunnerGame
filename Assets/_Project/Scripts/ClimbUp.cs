using UnityEngine;

public class ClimbUp : MonoBehaviour {
    [SerializeField] private float climbSpeed = 5f;

    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;

    private bool isGrounded;
    private Vector3 velocity;
    private CharacterController controller;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (velocity.y < 0 && isGrounded) {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void OnTriggerStay(Collider other) {

        // set for ladders and wall climbing
        if (other.CompareTag("ClimbToTop")) {
            // Move the player up while they are in the trigger
            controller.Move(Vector3.up * climbSpeed * Time.deltaTime);

            if (anim != null) {
                anim.SetBool("IsClimbing", true);
            }
        }

        // set for water slide
        if (other.CompareTag("WaterSlide")) {
            if (anim != null && isGrounded) {
                anim.SetBool("IsSliding", true);
            }
        }

        if (other.CompareTag("Water")) {
            if (anim != null) {
                anim.SetBool("IsSwimming", true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {

        // set for ladders and wall climbing
        if (other.CompareTag("ClimbToTop")) {
            // Stop climbing when the player exits the trigger
            if (anim != null) {
                anim.SetBool("IsClimbing", false);
            }
        }

        // set for water slide
        if (other.CompareTag("WaterSlide")) {
            if (anim != null) {
                anim.SetBool("IsSliding", false);
            }
        }


        if (other.CompareTag("Water")) {
            if (anim != null) {
                anim.SetBool("IsSwimming", false);
            }
        }
    }
}
