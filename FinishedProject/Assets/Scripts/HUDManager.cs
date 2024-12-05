using UnityEngine;
using TMPro; // For TextMeshProUGUI

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;     // Use TextMeshProUGUI instead of Text
    public TextMeshProUGUI ammoText;     // Use TextMeshProUGUI instead of Text
    public TextMeshProUGUI reloadText;   // Use TextMeshProUGUI instead of Text

    private WaveManager waveManager;
    private PlayerShooting playerShooting;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        playerShooting = FindObjectOfType<PlayerShooting>();
        reloadText.gameObject.SetActive(false); // Hide reload text initially
    }

    void Update()
    {
        // Update wave text with current wave
        waveText.text = "Wave: " + waveManager.GetCurrentWave();

        // Update ammo text with current ammo count
        ammoText.text = "Ammo: " + playerShooting.GetCurrentAmmo() + "/" + playerShooting.maxAmmo;

        // Check if player is reloading
        if (playerShooting.IsReloading())
        {
            reloadText.gameObject.SetActive(true); // Show reload text
            reloadText.text = "Reloading..."; // Update reload text
        }
        else
        {
            reloadText.gameObject.SetActive(false); // Hide reload text if not reloading
        }
    }
}
