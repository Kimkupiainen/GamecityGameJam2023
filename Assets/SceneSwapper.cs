using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    private static SceneSwapper instance;

    private void Awake()
    {
        // Check if an instance of the GameManager already exists
        if (instance != null && instance != this)
        {
            // Destroy this instance to enforce the Singleton pattern
            Destroy(gameObject);
            return;
        }

        // Set this instance as the Singleton instance
        instance = this;

        // Persist this game object throughout scene changes
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Check for scene reload input
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Check for main menu input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the main menu scene (Scene 0)
            SceneManager.LoadScene(0);
        }
    }
}