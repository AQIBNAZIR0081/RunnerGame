using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterEnums charEnums;
    public CharacterController characterController;

    [Header("Audio Setting")]
    public AudioSource objSource;
    public AudioManager audioManager;

    [Header("Movement Settings")]
    public float speed = 10f;
    public float sidewaysSpeed = 10f;
    public float swipDistanceY = 100f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Header("Particle Setting")]
    public ParticleSystem particle;

    [Header("Object Switcher")]
    public GameObject buttonsPanel;

    private Rigidbody rb;
    private Animator anim;

    //private bool jumpAllowed;
    //private AudioSource objSound;
    //private Vector3 pointerStartPosition;
    //private Vector3 pointerEndPosition;
    //private Vector3 deltaPosition;
    //private bool isMovingLeft = false;
    //private bool isMovingRight = false;
    private void OnEnable() {
        // Re-acquire references when object becomes active
        if (characterController == null) {
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        objSource = GetComponent<AudioSource>();

        if (characterController == null) {
            characterController = GetComponent<CharacterController>();
        }

    }


    private void Update() {
        if (TaptoStart.instance.isGameStart) {
            SwipeController.Instance.TouchesInput(transform.gameObject);
        }

        //SwipController();
    }

    private void FixedUpdate() {

        if (!gameObject.activeInHierarchy) return;

        if (TaptoStart.instance.isGameStart) {
            buttonsPanel.SetActive(true);

            // play audio according to active object
            if (audioManager != null) {
                audioManager.PlayAudioForRespectiveObject(charEnums, objSource);
            }

            if (anim != null && charEnums == CharacterEnums.Person)
                anim.SetBool("IsRunning", true);

            if (characterController != null) {
                Vector3 movementDirection = transform.forward;
                if (characterController != null && characterController.enabled && characterController.gameObject.activeInHierarchy) {
                    characterController.Move(movementDirection * speed * Time.fixedDeltaTime);
                }

            }
            else {
                // Move the player in z direction based on speed
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, transform.forward.z * speed);
            }


            //if (rb.position.y < -0.5f) {
            //    if (anim != null && charEnums == CharacterEnums.Person)
            //        anim.SetBool("IsRunning", false);
            //    GameManager.Instance.LoseGame();
            //}
        }
        else {
            if (anim != null)
                anim.Play("Idle");
        }

    }
    

    //private void Jump()
    //{
    //    if (jumpAllowed)
    //    {
    //        if(jumpSound != null) 
    //            jumpSound.Play();

    //        particle.Play();
    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //        anim.SetTrigger("IsJumped");

    //        jumpAllowed = false;
    //    }
    //}

    //private void SwipController()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        // Touch Began Phase
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            pointerStartPosition = touch.position;
    //        }


    //        // Touch Move or stationary Phase
    //        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    //        {
    //            deltaPosition = touch.deltaPosition;

    //            Vector3 moveDirection = new Vector3(deltaPosition.x, 0, 0);

    //            if (transform.position.x > minClampPosition || transform.position.x < maxClampPosition)
    //            {
    //                transform.position = new Vector3(
    //                    Mathf.Clamp(transform.position.x, minClampPosition, maxClampPosition),
    //                    transform.position.y,
    //                    transform.position.z
    //                );
    //                transform.Translate(moveDirection * Time.deltaTime);
    //            }

    //        }

    //        // Touch Ended Phase
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            pointerEndPosition = touch.position;

    //            Vector3 pointerYend = new Vector3(0, pointerEndPosition.y, 0);
    //            Vector3 pointerYstart = new Vector3(0, pointerStartPosition.y, 0);

    //            float swipDiffVerticle = (pointerYend - pointerYstart).magnitude;

    //            if (pointerEndPosition.y > pointerStartPosition.y && swipDiffVerticle > swipDistanceY && rb.linearVelocity.y == 0)
    //            {
    //                jumpAllowed = true;
    //            }
    //        }

    //    }
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
