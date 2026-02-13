using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float sidewaysSpeed = 10f;
    public float swipDistanceY = 100f;

    [Space]
    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Space]
    [Header("Clamping Setting")]
    public float minClampPosition = -3f;
    public float maxClampPosition = 3f;

    [Header("Audio Setting")]
    public AudioSource jumpSound;

    [Header("Particle Setting")]
    public ParticleSystem particle;

    //[Header("Tap to Start")]
    //public Animator animator;
    //public bool isGameStarted;
    //public GameObject tapToStartBtn;

    private bool jumpAllowed;
    private Rigidbody rb;
    private Animator anim;
    private Vector3 pointerStartPosition;
    private Vector3 deltaPosition;
    private Vector3 pointerEndPosition;

    //private bool isMovingLeft = false;
    //private bool isMovingRight = false;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //animator = tapToStartBtn.GetComponent<Animator>();
    }

    private void Update()
    {
        SwipController();
    }

    private void FixedUpdate()
    {
        if(TaptoStart.instance.isGameStart)
        {
            anim.SetBool("IsRunning", true);
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

            Jump();

            if (rb.position.y < -0.3f)
            {
                anim.SetBool("IsRunning", false);
                GameManager.Instance.LoseGame();
            }
        }
        else
        {
            anim.Play("Idle");
        }


    }

    private void SwipController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Touch Began Phase
            if (touch.phase == TouchPhase.Began)
            {
                pointerStartPosition = touch.position;
            }


            // Touch Move or stationary Phase
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                deltaPosition = touch.deltaPosition;

                Vector3 moveDirection = new Vector3(deltaPosition.x, 0, 0);

                if (transform.position.x > minClampPosition || transform.position.x < maxClampPosition)
                {
                    transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x, minClampPosition, maxClampPosition),
                        transform.position.y,
                        transform.position.z
                    );
                    transform.Translate(moveDirection * Time.deltaTime);
                }

            }

            // Touch Ended Phase
            if (touch.phase == TouchPhase.Ended)
            {
                pointerEndPosition = touch.position;

                Vector3 pointerYend = new Vector3(0, pointerEndPosition.y, 0);
                Vector3 pointerYstart = new Vector3(0, pointerStartPosition.y, 0);

                float swipDiffVerticle = (pointerYend - pointerYstart).magnitude;

                if (pointerEndPosition.y > pointerStartPosition.y && swipDiffVerticle > swipDistanceY && rb.linearVelocity.y == 0)
                {
                    jumpAllowed = true;
                }
            }

        }
    }

    private void Jump()
    {
        if (jumpAllowed)
        {
            jumpSound.Play();
            particle.Play();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("IsJumped");

            jumpAllowed = false;
        }
    }

    //public void TapToStart()
    //{
    //    isGameStarted = true;
    //    tapToStartBtn.SetActive(false);
    //}

    #region ButtonInput
    /*
    // Update is called once per frame
    void Update()
    {
        // Move the player in z direction based on speed
        Vector3 movementDirection = transform.forward;

        transform.Translate( movementDirection * speed * Time.deltaTime);

        // Move the player in x direction based on button hold
        if (isMovingRight)
        {
            particle.Play();
            transform.position += Vector3.right * sidewaysSpeed * Time.deltaTime;
        }

        // Move the player in -x direction based on button hold
        if (isMovingLeft)
        {
            particle.Play();
            transform.position += Vector3.left * sidewaysSpeed * Time.deltaTime;
        }

    }

    public void SetMoveLeft(bool state)
    {
        isMovingLeft = state;
    }

    public void SetMoveRight(bool state)
    {
        isMovingRight = state;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            jumpSound.Play();
            particle.Play();
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }
    */
    #endregion

    #region MoveLeftOrRight
    /*
    public void MoveLeft()
    {
        transform.position += Vector3.left * rotationSpeed * Time.deltaTime;
    }

    // Move to Right on button press
    public void MoveRight()
    {
        transform.position += Vector3.left * rotationSpeed * Time.deltaTime;
    }
    */
    #endregion

}
