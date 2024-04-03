using UnityEngine;
using UnityEngine.Events;

public class NPCAI : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex;
    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    Vector2 moveDirection;

    [Header("Controls")]
    [SerializeField] private bool canMove;

    [Header("Events")]
    public UnityEvent onEnd; // Event to trigger when the dialogue ends

    private void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player");
        target = player.transform;

        waypointIndex = 0;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.position) <= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.eulerAngles = target.eulerAngles;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) >= followDistance)
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            anim.SetBool("Idle", true);
        }
        else
        {
            if (!canMove)
            {
                return;
            }

            anim.SetBool("Idle", false);

            moveDirection = target.position - transform.position;

            anim.SetFloat("Horizontal", moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.y);
            anim.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }

    public void LookAtPlayer()
    {
        Vector2 lookDirection = target.position - transform.position;

        anim.SetFloat("LastMoveX", lookDirection.x);
        anim.SetFloat("LastMoveY", lookDirection.y);
    }

    public void LookAtNPC(Transform npc)
    {
        Vector2 lookDirection = npc.position - transform.position;

        anim.SetFloat("LastMoveX", lookDirection.x);
        anim.SetFloat("LastMoveY", lookDirection.y);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        waypointIndex++;

        if (waypointIndex >= waypoints.Length)
        {
            canMove = false;

            waypointIndex = 0;
        }
        else
        {
            target = waypoints[waypointIndex];
        }
    }

    public void SetLookDirection(Vector2 lookDirectionX, Vector2 lookDirectionY)
    {
        anim.SetFloat("LastMoveX", lookDirectionX.x);
        anim.SetFloat("LastMoveY", lookDirectionY.y);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetVisiblity(bool isVisible)
    {
        if (isVisible)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            canMove = false;

            SetWaypoint();

            canMove = true;
        }
    }

    public void EndDialogue()
    {
        if (onEnd != null)
        {
            onEnd.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
