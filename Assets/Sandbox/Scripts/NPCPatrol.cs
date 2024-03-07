using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;    // Array of patrol points
    public float moveSpeed = 3f;        // Speed of NPC movement
    public Animator animator;
    public NPCHealth otherScript;
    private int currentPatrolIndex = 0; // Index of the current patrol point
    private int direction = 1;           // Movement direction (1 for forward, -1 for backward)
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (otherScript.IsAlive == true)
        {// Check if there are patrol points defined
            if (patrolPoints.Length > 0)
            {
                // Calculate the next target position
                Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;
                Vector3 nextTargetPosition = patrolPoints[currentPatrolIndex + direction].position;

                // Move NPC towards the next target position
                transform.position = Vector3.MoveTowards(transform.position, nextTargetPosition, moveSpeed * Time.deltaTime);
                if (direction == -1)
                {
                    spriteRenderer.flipX = true; // Flip sprite when moving left
                }
                else
                {
                    spriteRenderer.flipX = false; // Do not flip sprite when moving right
                }
                // Check if NPC has reached the next patrol point
                if (Vector3.Distance(transform.position, nextTargetPosition) <= 0.5f)
                {
                    currentPatrolIndex += direction;

                    // Reverse movement direction when reaching the end or start of patrol path
                    if (currentPatrolIndex >= patrolPoints.Length - 1)
                    {
                        direction = -1;
                    }
                    else if (currentPatrolIndex <= 0)
                    {
                        direction = 1;
                    }
                }
            }
            else
            {
                Debug.LogWarning("No patrol points defined for NPC: " + gameObject.name);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger fight animation
            animator.SetTrigger("fight");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger fight animation
            animator.SetTrigger("normal");
        }
    }
}
