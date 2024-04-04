using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float movementSpeed = 2f; // Movement speed of the NPC
    public float attackRange = 1.5f; // Attack range of the NPC
    public float damageAmount = 10f; // Damage amount for NPC's attack
    public NPCHealth otherScript;
    private Transform player; // Reference to the player's transform
    private bool isAttacking; // Flag to track if NPC is currently attacking

    void Start()
    {
        // Find the player GameObject by tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (otherScript.IsAlive == true)
        {
            // Check if the player is within attack range
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                // Move towards the player
                Vector2 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * movementSpeed * Time.deltaTime);

                // Face the player
                if (direction.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else if (direction.x > 0)
                    transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else { }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (otherScript.IsAlive == true)
        {
            // Check if collided with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Attack the player
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
            else { }
        }
    }
}
