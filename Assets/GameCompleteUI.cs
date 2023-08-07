using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteUI : MonoBehaviour
{
    private int nextLevel;
    [SerializeField] AudioSource levelComplete;

    private void Start()
    {
        levelComplete.Play();
        // Get the index of the previous scene from PlayerPrefs
        nextLevel = PlayerPrefs.GetInt("NextLevel");
    }

    public void GameComplete()
    {
        SceneManager.LoadScene(nextLevel);
    }
}