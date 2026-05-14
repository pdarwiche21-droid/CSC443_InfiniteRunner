using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Added this for the new Input System

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
        if (deathScreenPanel != null) deathScreenPanel.SetActive(false);
        if (pauseScreenPanel != null) pauseScreenPanel.SetActive(false);

        // Ensure cursor is hidden when game starts
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Update distance score
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver && !isPaused)
        {
            distanceText.text = Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";
        }

        // 2. Handle Space Bar for Pausing (New Input System fix)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!GameManager.Instance.isGameOver)
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseScreenPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseScreenPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowDeathScreen()
    {
        deathScreenPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset time so the menu isn't frozen 
        SceneManager.LoadScene("MainMenu"); // Make sure your menu scene is named exactly this 
    }

}