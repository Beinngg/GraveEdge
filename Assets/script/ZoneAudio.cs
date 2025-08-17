using UnityEngine;

public class ZoneAudio : MonoBehaviour
{
    public AudioSource zoneAudio;       // Drag an AudioSource here
    public string playerTag = "Player"; // Tag your player object with "Player"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!zoneAudio.isPlaying)
            {
                zoneAudio.Play();
                Debug.Log("🎵 Zone audio started");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (zoneAudio.isPlaying)
            {
                zoneAudio.Stop();
                Debug.Log("🔇 Zone audio stopped");
            }
        }
    }
}
