using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    private float currentHealth; 
    public Animator animator; // Current health
    public bool IsAlive = true;
    void Start()
    {
        
        currentHealth = maxHealth; // Initialize current health
    }

    public void TakeDamage(float amount)
    {
        // Reduce health by the amount of damage
        currentHealth -= amount;

        // Check if NPC health has reached zero
        if (currentHealth <= 0)
        {
            // NPC is defeated, perform any necessary actions (e.g., play death animation, deactivate NPC)
            //gameObject.SetActive(false);
            IsAlive = false;
            animator.SetTrigger("Death");

        }
    }
}
