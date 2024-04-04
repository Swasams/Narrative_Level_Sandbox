using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damageAmount = 10f; // Damage amount for player's attack
    public float attackRange = 3f;
    void Update()
    {
        // Check if the player presses the "E" key to attack
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Perform attack if colliding with an enemy
            Attack();
        }
    }

    void Attack()
    {
        // Check if the player is colliding with an enemy
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
          
                // Get the enemy's health component
                NPCHealth enemyHealth = collider.GetComponent<NPCHealth>();
                if (enemyHealth != null)
                {
                    // Deal damage to the enemy
                    enemyHealth.TakeDamage(damageAmount);
                }
            
        }
    }
}
