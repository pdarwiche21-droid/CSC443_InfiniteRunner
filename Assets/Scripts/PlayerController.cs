using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField, Min(1)] private int laneCount = 3;
    [SerializeField] private float laneSwitchSpeed = 14f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 8f;
    [SerializeField] private float gravity = -25f;

    [Header("Animations")]
    [SerializeField] private Animator animator; 
    [SerializeField] private float slideDuration = 1.0f;

    private int _laneIndex;
    private float _y;
    private float _yVel;
    private Vector2 _prevMove;

    void Awake()
    {
        //  drive position directly any rigidbody on  object must be kinematic
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        if (v.x > 0.5f && _prevMove.x <= 0.5f) ChangeLane(+1);
        else if (v.x < -0.5f && _prevMove.x >= -0.5f) ChangeLane(-1);
        if (v.y > 0.5f && _prevMove.y <= 0.5f && _y <= 0f) _yVel = jumpVelocity;

        if (v.y < -0.5f && _prevMove.y >= -0.5f && _y <= 0f)
        {
            StartSlide();
        }
        _prevMove = v;
    }

    private void ChangeLane(int delta)
    {
        int half = laneCount / 2;
        _laneIndex = Mathf.Clamp(_laneIndex + delta, -half, half);
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the bunny's trigger touches the spike's trigger
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Slime Rabbit hit a spike!");

        // Hide the Rabbit
        Transform model = transform.Find("Model");
        if (model != null) model.gameObject.SetActive(false);

        // Stop the game
        Time.timeScale = 0;

        //  Restart the level after 2 seconds
        
        Invoke("RestartLevel", 2f);
    }
    private void StartSlide()
    {
        if (animator != null)
        {
            animator.SetTrigger("isSliding");
        }

        // Logic to shrink collider so you fit under the arch
        StartCoroutine(SlideCoroutine());
    }

    private System.Collections.IEnumerator SlideCoroutine()
    {
        // Change SphereCollider to CapsuleCollider
        CapsuleCollider col = GetComponent<CapsuleCollider>();

        if (col != null)
        {
            float originalHeight = col.height;
            Vector3 originalCenter = col.center;

            // 1. Shrink the height and lower the center so it's on the floor
            col.height = originalHeight * 0.5f;
            col.center = new Vector3(originalCenter.x, originalCenter.y * 0.5f, originalCenter.z);

            yield return new WaitForSeconds(slideDuration);

            // 2. Return to normal
            col.height = originalHeight;
            col.center = originalCenter;
        }
    }
    private void RestartLevel()
    {
        Time.timeScale = 1f; // IMPORTANT: Reset time or the game stays frozen!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        _yVel += gravity * Time.deltaTime;
        _y += _yVel * Time.deltaTime;
        if (_y < 0f) { _y = 0f; _yVel = 0f; }

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, _laneIndex * laneOffset, laneSwitchSpeed * Time.deltaTime);
        pos.y = _y;
        pos.z = 0f;
        transform.position = pos;
    }
}
