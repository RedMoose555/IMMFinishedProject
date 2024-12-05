using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    private int damage = 1; // Default damage

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {
            GhostController ghost = other.GetComponent<GhostController>();

            if (ghost != null)
            {
                ghost.TakeDamage(damage); // Apply the current damage
            }

            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage; // Update the bullet's damage
    }
}
