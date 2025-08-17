using UnityEngine;

public class PlayAudioOnZoneEnter : MonoBehaviour
{
    public AudioSource audioSource;     // Drag your AudioSource here
    public string playerTag = "Player"; // Tag your player with "Player"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("🎵 Audio started because player entered the zone.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("🔇 Audio stopped because player left the zone.");
            }
        }
    }
}
