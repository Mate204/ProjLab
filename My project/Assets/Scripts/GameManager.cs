using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI inGameScoreText;
    public AudioSource audioSource;


    private float gameStartTime;
    private float elapsedTime;
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            if (Instance != this) 
            {
                Destroy(gameObject);
                return;
            }
               
        }

        gameStartTime = Time.time;
        Time.timeScale = 1f;

        if (gameOverPanel != null)
        { 
            gameOverPanel.SetActive(false);
        }
    
    }
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            elapsedTime = Time.time - gameStartTime;
            
            if (inGameScoreText != null)
            {
                inGameScoreText.text = $"Time: {FormatTime(elapsedTime)}";
            }
        }
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
 
        audioSource.Play();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            scoreText.text = $"Time Survived: {FormatTime(elapsedTime)}";
        }
        Debug.Log("Game Over! Restarting the game...");
        // Add logic to restart the game or show game over UI
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return $"{minutes:D2} min {seconds:D2} sec";
    }
}
