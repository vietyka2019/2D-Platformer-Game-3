using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private int previousSceneIndex;

    private void Start()
    {
        // Get the index of the previous scene from PlayerPrefs
        previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex");
    }

    public void GameOver()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(previousSceneIndex);
    }
}
