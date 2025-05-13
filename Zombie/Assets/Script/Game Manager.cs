using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UIManager UIManager;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public LootTable lootTable;
    public BuffsHandler buffsHandler;
    public NameInput nameInput;
    public Leaderboard leaderboard;
    public GameObject gameOverButton, pauseButton;
    public EventSystemAcess eventSystemAcess;
    public bool isPaused, gameOver;

    internal GameObject player;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if ((Instance != null && Instance != this) || SceneManager.GetActiveScene().buildIndex == 0)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Joystick1Button0) && Input.GetKeyDown(KeyCode.Joystick1Button1) && Input.GetKeyDown(KeyCode.Joystick1Button2)))  
        {
            PauseScreen(pauseScreen);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        Toggle(gameOverScreen);
        eventSystemAcess.Select(gameOverButton);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);

        ScoreEntry scoreEntry = new()
        {
            score = UIManager.score,
            playerName = nameInput.GetName()
        };

        leaderboard.SaveFile(scoreEntry);
        Destroy(gameObject);
        Destroy(player);
        SceneManager.LoadScene(0);
    }

    public void TryAgain()
    {
        Time.timeScale = 1; 
        gameOverScreen.SetActive(false);
        
        if (isPaused)
            PauseScreen(pauseScreen);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseScreen(GameObject gameObject)
    {
        if (gameOver)
            return;

        Toggle(gameObject);
        eventSystemAcess.Select(pauseButton);
        isPaused = !isPaused;
        Time.timeScale = !isPaused ? 1 : 0;
    }

    public void Toggle(GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf); 
}
