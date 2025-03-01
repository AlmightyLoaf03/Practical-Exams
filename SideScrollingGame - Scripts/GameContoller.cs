using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour
{
    private int progressAmount = 0;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    private int currentLevelIndex = 0;

    public GameObject gameOverScreen;

    public static event Action OnReset;

    void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        //progress bar
        progressAmount = 0;
        progressSlider.value = 0;

        //event subsriptions
        Gem.OnGemCollect += IncreaseProgressAmount;
        HoldToLoad.OnHoldComplete += LoadNextLevel;
        PlayerHealth.OnPlayerDied += GameOverScreen;

        gameOverScreen.SetActive(false);
        LoadCanvas.SetActive(false);
    }

    void GameOverScreen()
    {
        if (gameOverScreen != null) // Check if gameOverScreen exists before modifying it
        {
            gameOverScreen.SetActive(true);
            MusicManager.PauseBackgroundMusic();
        }
        else
        {
            Debug.LogWarning("GameOverScreen is missing! Make sure it's assigned in the inspector.");
        }

        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        MusicManager.PlayBackgroundMusic(true);
        progressAmount = 0; //resets progress
        SceneManager.LoadScene(currentLevelIndex); //reloads the next scene
        OnReset?.Invoke();
        Time.timeScale = 1;
    }

    void IncreaseProgressAmount(int amount)
    {
        if (this == null) return; // Ensure the GameController still exists

        progressAmount += amount;
        if (progressSlider != null)
        {
            progressSlider.value = progressAmount;
        }

        if (progressAmount >= 100 && LoadCanvas != null)
        {
            LoadCanvas.SetActive(true);
            Debug.Log("Level Complete!");
        }
    }
    //void LoadLevel(int level)
    //{
        //levels[currentLevelIndex].gameObject.SetActive(false);
        //levels[level].gameObject.SetActive(true);

        //player.transform.position = new Vector3(0, 0, 0);

        //currentLevelIndex = level;
        //progressAmount = 0;
        //progressSlider.value = 0;
   //}

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex + 1);

        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }
}
