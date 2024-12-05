using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject ghostPrefab; // Ghost prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points
    public int initialGhostCount = 3; // Number of ghosts in the first wave
    public float timeBetweenWaves = 5f; // Time in seconds between waves
    public float spawnDelay = 1.5f; // Delay between individual ghost spawns

    private int currentWave = 1; // Current wave number
    private int ghostCount; // Number of ghosts to spawn this wave
    private bool isWaveActive = false;

    void Start()
    {
        // Start the first wave as soon as the game starts
        StartWave();
    }

    void Update()
    {
        // Check if all ghosts are destroyed (or dead)
        if (isWaveActive && GameObject.FindGameObjectsWithTag("ghost").Length == 0)
        {
            EndWave();
        }
    }

    // Start a new wave
    void StartWave()
    {
        isWaveActive = true;

        // Increase ghost count each wave
        ghostCount = initialGhostCount + (currentWave - 1) * 2;

        // Start spawning ghosts with a delay
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        for (int i = 0; i < ghostCount; i++)
        {
            // Select a random spawn point from the list of spawn points
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            // Instantiate the ghost at the chosen spawn point
            Instantiate(ghostPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);

            // Wait for the delay before spawning the next ghost
            yield return new WaitForSeconds(spawnDelay);
        }

        // Mark wave as active once all ghosts are spawned
        isWaveActive = true;
    }

    // End the current wave and prepare the next one
    void EndWave()
    {
        isWaveActive = false;
        currentWave++; // Move to the next wave
        Invoke(nameof(StartWave), timeBetweenWaves); // Start the next wave after a short delay
    }

    // Public method to get the current wave number
    public int GetCurrentWave()
    {
        return currentWave;
    }
}
