using UnityEngine;

public class AttackAudioController : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector
    public AudioClip attackClip;    // Assign your LMB sound effect in Inspector

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogWarning("[AttackAudioController] No AudioSource found. Please assign one.");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // LMB pressed
        {
            PlayAttackSound();
        }
    }

    void PlayAttackSound()
    {
        if (audioSource != null && attackClip != null)
        {
            audioSource.clip = attackClip;
            audioSource.Play();
        }
    }
}
