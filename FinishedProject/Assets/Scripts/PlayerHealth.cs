using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 10; // Maximum health
    private int currentHealth; // Current health

    public float invincibilityDuration = 2f; // Time in seconds the player is invincible after taking damage
    private bool isInvincible = false; // To track invincibility state

    void Start()
    {
        currentHealth = MaxHealth; // Initialize health
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // Ignore damage if invincible

        currentHealth -= amount;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityTimer());
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene"); // Load Game over scene
    }

    private System.Collections.IEnumerator InvincibilityTimer()
    {
        isInvincible = true; // Activate invincibility
        yield return new WaitForSeconds(invincibilityDuration); // Wait for the invincibility duration
        isInvincible = false; // Deactivate invincibility
    }
}
