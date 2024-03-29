using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private InputActionReference walk;
    [SerializeField] private InputActionReference run;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("Particles")]
    [SerializeField] private ParticleSystem dust;

    [Header("Paused")]
    public bool isPaused;

    public LayerMask collisionLayers;

    private Vector2 moveDirection;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isPaused)
        {
            return;
        }

        if (!anim.GetBool("Running"))
        {
            rb.velocity = new Vector2(moveDirection.x * walkSpeed, moveDirection.y * walkSpeed);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);
        }
    }

    void Update()
    {
        moveDirection = walk.action.ReadValue<Vector2>();

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", moveDirection.x);
        anim.SetFloat("Vertical", moveDirection.y);
        anim.SetFloat("Speed", moveDirection.sqrMagnitude);

        if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }

        if (anim.GetFloat("Speed") == 0)
        {
            anim.SetBool("Running", false);
        }

        if (anim.GetFloat("Vertical") >= 0.1)
        {
            if (anim.GetFloat("Speed") >= 0.01)
            {
                CreateDust();
            }
        }

        if (anim.GetFloat("Vertical") <= -0.1)
        {
            if (anim.GetFloat("Speed") >= 0.01)
            {
                CreateDust();
            }
        }

        if (anim.GetFloat("Horizontal") <= -0.1)
        {
            if (anim.GetFloat("Speed") >= 0.01)
            {
                CreateDust();
            };
        }

        if (anim.GetFloat("Horizontal") >= 0.1)
        {
            if (anim.GetFloat("Speed") >= 0.05)
            {
                CreateDust();
            }
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }

    private void OnEnable()
    {
        run.action.started += Run;
    }

    private void OnDisable()
    {
        run.action.started -= Run;
    }

    private void Run(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            return;
        }

        anim.SetBool("Running", true);
    }
}