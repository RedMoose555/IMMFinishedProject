using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    // List of scene names where the cursor should be locked
    private string[] lockedScenes = { "Forest", "Winter", "Desert" };

    void Start()
    {
        // Ensure the cursor is locked or unlocked based on the scene
        SetCursorLock();

        // Subscribe to the sceneLoaded event to handle cursor settings when scenes change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetCursorLock(); // Re-apply the cursor locking after the scene is loaded

        // Specifically handle the Game Over scene (force cursor visible and unlocked)
        if (scene.name == "Game Over")
        {
            // Automatically unlock the cursor if the player reaches the Game Over scene
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        // Check if the player presses the 'E' key to unlock the cursor
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnlockCursor();
        }
    }

    // This method locks or unlocks the cursor based on the current scene
    void SetCursorLock()
    {
        if (IsSceneLocked())
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center
            Cursor.visible = false; // Hide the cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            Cursor.visible = true; // Make the cursor visible
        }
    }

    bool IsSceneLocked()
    {
        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Check if the current scene is in the lockedScenes list
        foreach (string scene in lockedScenes)
        {
            if (currentScene == scene)
            {
                return true;
            }
        }

        return false;
    }

    // Method to unlock the cursor
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Make the cursor visible
    }
}
