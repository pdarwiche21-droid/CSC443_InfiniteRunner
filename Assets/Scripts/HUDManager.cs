using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] private TextMeshProUGUI distanceText;

    [Header("Death Screen UI")]
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Pause Screen UI")]
    [SerializeField] private GameObject pauseScreenPanel;

    private bool isPaused = false;

    void Start()
    {
        // Ensure panels are hidden at start
        if (deathScreenPanel != null) deathScreenPanel.SetActive(false);
        if (pauseScreenPanel != null) pauseScreenPanel.SetActive(false);
    }

    void Update()
    {
        // 1. Update distance score if the game is active
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver && !isPaused)
        {
            distanceText.text = Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";
        }

        // 2. Handle Space Bar for Pausing (Extension C)
        // We only allow pausing if the player hasn't already died
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isGameOver)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // --- PAUSE LOGIC ---
    public void PauseGame()
    {
        isPaused = true;
        pauseScreenPanel.SetActive(true);

        // Freeze time and physics
        Time.timeScale = 0f;

        // Show cursor for menu interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseScreenPanel.SetActive(false);

        // Unfreeze time
        Time.timeScale = 1f;

        // Hide cursor during gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // --- DEATH LOGIC ---
    public void ShowDeathScreen()
    {
        deathScreenPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // --- BUTTON FUNCTIONS ---
    public void RestartGame()
    {
        Time.timeScale = 1f; // Essential: reset time before reloading!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Main Menu Scene...");
        // SceneManager.LoadScene("MainMenu"); // Uncomment when the menu scene is created
    }
}