using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Reference to the power-up prefab
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 20f; // Time between power-up spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), spawnInterval, spawnInterval);
    }

    void SpawnPowerUp()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(powerUpPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}
