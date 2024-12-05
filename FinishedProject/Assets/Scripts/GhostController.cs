using UnityEngine;

public class GhostController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public GameObject explosionEffect;  // Reference to the explosion particle effect prefab

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Ghost movement logic
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Trigger explosion effect when the ghost dies
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);  // Instantiate at ghost's position
        }

        // Destroy the ghost after explosion effect
        Destroy(gameObject);
    }
}
