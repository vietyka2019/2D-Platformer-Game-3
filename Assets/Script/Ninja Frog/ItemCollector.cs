using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text appleCollected;
    [SerializeField] AudioSource collectAppleSoundEffect;

    int appleCount = 0;


    private void Update()
    {
        GameComplete();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple")) // check if the gameObject collides with has an Apple tag
        {
            collectAppleSoundEffect.Play(); 
            Destroy(collision.gameObject); // destroy that game object in case the if condition is true
            appleCount++;
            appleCollected.text = $"{appleCount}";
        }
    }

    private void GameComplete()
    {
        if (appleCount == 12)
        {
            // Get the name of the current scene
            string currentSceneName = SceneManager.GetActiveScene().name;
            int gameCompleteIndex = 0;
            Debug.Log("currentSceneName: " + currentSceneName); 

            // Check if the current scene is Level 10 or other levels
            switch (currentSceneName)
            {
                case "Level 10":
                    gameCompleteIndex = SceneManager.sceneCountInBuildSettings - 3;
                    SceneManager.LoadScene(gameCompleteIndex); // naviage to the game over scnene
                    break;
                default:
                    PlayerPrefs.SetInt("NextLevel", SceneManager.GetActiveScene().buildIndex + 1);
                    PlayerPrefs.Save();
                    gameCompleteIndex = SceneManager.sceneCountInBuildSettings - 2;
                    SceneManager.LoadScene(gameCompleteIndex); // naviage to the game over scnene
                    break; 
            }
        } 
    }                                                                                                                                                                                 
}