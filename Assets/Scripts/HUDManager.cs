using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed for Restart

public class HUDManager : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] private TextMeshProUGUI distanceText;

    [Header("Death Screen UI")]
    [SerializeField] private GameObject deathScreenPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver)
        {
            distanceText.text = Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";
        }
    }

    // Call this from PlayerController.Die()
    public void ShowDeathScreen()
    {
        deathScreenPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";

        // Show the cursor so the player can click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Button Functions
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");
        // SceneManager.LoadScene("MainMenu"); // Uncomment this when menu is ready
    }
}