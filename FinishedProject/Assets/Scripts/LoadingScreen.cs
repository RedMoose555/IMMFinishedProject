using UnityEngine;
using UnityEngine.UI;  // for UI elements
using UnityEngine.SceneManagement;  // for scene management
using TMPro;  // for TextMeshPro
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen panel
    public Image progressBar; // Reference to the progress bar
    public TextMeshProUGUI progressText; // Use TextMeshProUGUI, not Text
    public string sceneToLoad; // The scene to load

    public Sprite forestImage; // Reference to the Forest background image
    public Sprite winterImage; // Reference to the Winter background image
    public Sprite desertImage; // Reference to the Desert background image

    private void Start()
    {
        // Set the loading screen as active at the start
        loadingScreen.SetActive(false);  // Start with the loading screen hidden
    }

    public void StartLoadingScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        // Show the loading screen
        loadingScreen.SetActive(true);

        // Set the background image based on the selected scene
        if (sceneToLoad == "Forest")
        {
            // Set the background to the Forest image
            progressBar.transform.parent.GetComponent<Image>().sprite = forestImage;
        }
        else if (sceneToLoad == "Winter")
        {
            // Set the background to the Winter image
            progressBar.transform.parent.GetComponent<Image>().sprite = winterImage;
        }
        else if (sceneToLoad == "Desert")  // Handle the Desert image
        {
            // Set the background to the Desert image
            progressBar.transform.parent.GetComponent<Image>().sprite = desertImage;
        }

        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Ensure the scene doesn't automatically switch
        operation.allowSceneActivation = false;

        // While the scene is still loading
        while (!operation.isDone)
        {
            // Update the progress bar
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progress;

            // Update the progress text
            progressText.text = Mathf.FloorToInt(progress * 100f) + "%";

            // If the loading is at 90% (ready to activate the scene), we delay a few seconds
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Press any key to continue...";

                // Wait for a moment to simulate the loading delay
                yield return new WaitForSeconds(4f);

                // Allow the scene to activate
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
