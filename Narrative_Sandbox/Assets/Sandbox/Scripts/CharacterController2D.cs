using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the animator component

    [SerializeField] private float m_JumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;

    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogWarning("Animator reference not set in CharacterController2D script!");
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }

    public void Move(float move, bool jump, bool punch, bool climb, bool talk)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator reference is null!");
            return;
        }

        Debug.Log("Move called with move = " + move + ", jump = " + jump + ", punch = " + punch + ", climb = " + climb + ", talk = " + talk);

        // Set animation parameters based on player input or state
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("Grounded", m_Grounded);
        animator.SetBool("Climbing", climb);
        animator.SetBool("Talking", talk);
        animator.SetBool("Punching", punch);

        // Move the character horizontally
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Flip the character if moving in the opposite direction
        if ((move > 0 && !m_FacingRight) || (move < 0 && m_FacingRight))
        {
            Flip();
        }

        // Apply jumping force if grounded and jump input is pressed
        if (m_Grounded && jump)
        {
            animator.SetTrigger("Jump");
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
