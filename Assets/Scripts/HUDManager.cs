using UnityEngine;
using TMPro; // Important for TextMeshPro

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            // Display distance as a whole number (e.g., "123m")
            distanceText.text = Mathf.FloorToInt(GameManager.Instance.Distance).ToString() + "m";
        }
    }
}