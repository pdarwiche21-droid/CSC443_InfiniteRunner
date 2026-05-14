using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameConfig config;

    [Header("Audio")]
    [SerializeField] private AudioSource levelMusic;

    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }
    public bool isGameOver { get; private set; } // Added this flag

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ScrollSpeed = config.startSpeed;
    }

    void Update()
    {
        if (isGameOver) return; // Stop everything if the player died

        ScrollSpeed = Mathf.Min(ScrollSpeed + config.speedIncreaseRate * Time.deltaTime, config.maxSpeed);
        Distance += ScrollSpeed * Time.deltaTime;
    }

    public void EndGame()
    {
        isGameOver = true;
        ScrollSpeed = 0;

        if (levelMusic != null)
        {
            levelMusic.Stop();
        }
    }
}
