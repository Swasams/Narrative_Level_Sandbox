using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float jumpForce = 10f; // Force applied when jumping
    public Transform groundCheck; // Reference to a GameObject to check if the player is grounded
    public LayerMask groundMask; // Mask of the ground layer
    public Animator animator; // Reference to the animator component
    [SerializeField] private float verticalSpeed = 5f; // Speed of vertical movement
    [SerializeField] private bool allowJumping = true;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float verticalInput;
    //private bool isJumping;
    private bool isLanding; public float VerticalSpeed
    {
        get { return verticalSpeed; }
        set { verticalSpeed = value; }
    }

    public bool AllowJumping
    {
        get { return allowJumping; }
        set { allowJumping = value; }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Set animation parameters based on movement
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Flip the character if moving left or right
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        // Jump
        if (allowJumping && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Punch
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Punch");
        }

        verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * moveSpeed);

        // Set vertical animation triggers
        if (verticalInput > 0) // Moving up
        {
            animator.SetBool("MovingUp", true);
            animator.SetBool("MovingDown", false);
        }
        else if (verticalInput < 0) // Moving down
        {
            animator.SetBool("MovingUp", false);
            animator.SetBool("MovingDown", true);
        }
        else // Not moving vertically
        {
            animator.SetBool("MovingUp", false);
            animator.SetBool("MovingDown", false);
        }
    }


    void FixedUpdate()
    {
        // Check if the player is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
        
        // Reset the landing trigger if the player is no longer landing
        if (isGrounded && !isLanding)
        {
            animator.ResetTrigger("Land");
        }

        // Set the landing flag based on the previous state of isGrounded
        if (!isGrounded && isLanding)
        {
            isLanding = false;
        }
        else if (isGrounded && !isLanding)
        {
            isLanding = true;
            animator.SetTrigger("Land");
            Invoke("ResetLandTrigger", 0.01f);
        }
    }
    void ResetLandTrigger()
    {
        animator.ResetTrigger("Land");
        //isLanding = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player exits collision with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
