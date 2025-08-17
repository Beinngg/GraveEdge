using UnityEngine;

public class TrainAudioController : MonoBehaviour
{
    public TrainMovement trainScript;
    public AudioSource trainAudio; // Drag your AudioSource here

    private bool audioPlaying = false;

    void Start()
    {
        // Make sure audio isn't playing at the start
        if (trainAudio.isPlaying)
            trainAudio.Stop();
    }

    void Update()
    {
        // Auto-stop if train has stopped moving
        if (!trainScript.startMoving && audioPlaying)
        {
            StopAudio();
        }
    }

    public void PlayAudio()
    {
        if (!audioPlaying)
        {
            trainAudio.Play();
            audioPlaying = true;
        }
    }

    public void StopAudio()
    {
        if (audioPlaying)
        {
            trainAudio.Stop();
            audioPlaying = false;
        }
    }
}
