using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health for the player
    private float currentHealth;   // Current health for the player
    public Animator animator;
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    public void TakeDamage(float amount)
    {
        // Reduce player's health by the amount of damage
        currentHealth -= amount;

        // Check if player health has reached zero
        if (currentHealth <= 0)
        {
            // Player is defeated, perform any necessary actions (e.g., play death animation, end game)
            Debug.Log("Player defeated!");
            animator.SetTrigger("Death");
        }
    }
}
