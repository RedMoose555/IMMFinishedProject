using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    public int maxAmmo = 20;
    public float reloadTime = 2f;
    public int normalDamage = 1; // Default bullet damage
    public int extraDamage = 2; // Damage during power-up

    private int currentDamage;
    private int currentAmmo;
    private float nextFireTime = 0f;
    private bool isReloading = false;

    // Sound-related variables
    public AudioClip shootSound;  // Sound effect for shooting
    private AudioSource audioSource;  // AudioSource to play the sound

    void Start()
    {
        currentAmmo = maxAmmo;
        currentDamage = normalDamage; // Initialize with normal damage

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentAmmo--;

        // Instantiate the bullet and shoot it
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDamage(currentDamage); // Pass the current damage to the bullet
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed;

        Destroy(bullet, 2f); // Destroy bullet after 2 seconds

        // Play shooting sound effect
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);  // Play the shoot sound
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public IEnumerator ActivateExtraDamage(float duration)
    {
        currentDamage = extraDamage; // Double the damage
        yield return new WaitForSeconds(duration); // Wait for the power-up duration
        currentDamage = normalDamage; // Revert to normal damage
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public bool IsReloading()
    {
        return isReloading;
    }
}
