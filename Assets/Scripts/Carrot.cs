using UnityEngine;


public class Carrot : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float floatAmplitude = 0.25f;
    [SerializeField] private float floatFrequency = 2f;

    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        // 1. Smooth Rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // 2. Smooth Floating (Sine Wave)
        Vector3 tempPos = _startPos;
        // Using Time.time here for smooth constant motion
        Vector3 currentLocalPos = transform.localPosition;
        currentLocalPos.y = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = currentLocalPos;
        transform.position = tempPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure your Slime Rabbit has the "Player" Tag in the Inspector!
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPickup();
        }

        // Despawn the carrot
        gameObject.SetActive(false);
    }
}