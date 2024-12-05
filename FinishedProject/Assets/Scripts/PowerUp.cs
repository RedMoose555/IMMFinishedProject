using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 10f; // Duration of the power-up effect

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the power-up
        if (other.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();

            if (playerShooting != null)
            {
                StartCoroutine(playerShooting.ActivateExtraDamage(duration));
            }

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}
