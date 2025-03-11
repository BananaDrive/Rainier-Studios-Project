using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public bool isPaused;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  
        {
            PauseTheScreen();
        } 
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void PauseTheScreen()
    {
        if (!isPaused && !gameOverScreen.activeInHierarchy)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}
