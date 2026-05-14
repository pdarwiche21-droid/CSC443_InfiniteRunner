using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX Clips")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip slideSound;
    [SerializeField] private AudioClip deathSound;

    private AudioSource _sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayJump() => PlaySound(jumpSound);
    public void PlaySlide() => PlaySound(slideSound);
    public void PlayDeath() => PlaySound(deathSound);

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
}