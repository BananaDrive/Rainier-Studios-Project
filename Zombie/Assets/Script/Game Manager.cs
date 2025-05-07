using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isPaused, gameOver;
    void Awake()
    {
        if ((Instance != null && Instance != this) || SceneManager.GetActiveScene().buildIndex == 0)
            Destroy(this);
        else
            Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  
        {
            PauseScreen(pauseScreen);
        } 
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        Toggle(gameOverScreen);
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

        Debug.Log(scoreEntry.playerName + " " + scoreEntry.score);
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
        isPaused = !isPaused;
        Time.timeScale = !isPaused ? 1 : 0;
    }

    public void Toggle(GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf); 
}
