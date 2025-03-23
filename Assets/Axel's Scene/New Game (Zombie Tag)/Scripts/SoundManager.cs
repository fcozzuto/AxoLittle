using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip loopClip; // Assign the audio clip in the Inspector

    void Start()
    {
        if (audioSource != null && loopClip != null)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true; // Enable looping
            audioSource.Play(); // Start playing the sound
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned.");
        }
    }
}

