using UnityEngine;

public class SprintAudioController : MonoBehaviour
{
    public AudioSource audioSource; // Assign in Inspector
    public AudioClip sprintClip;    // Assign sprint/dash sound effect here

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogWarning("[SprintAudioController] No AudioSource found. Please assign one.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlaySprintSound();
        }
    }

    void PlaySprintSound()
    {
        if (audioSource != null && sprintClip != null)
        {
            audioSource.clip = sprintClip;
            audioSource.Play();
        }
    }
}
