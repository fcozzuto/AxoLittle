using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip loopClip; // Assign the initial audio clip in the Inspector
    public AudioClip winningMusic;

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

    public void TriggerWin()
    {
        FadeToNewMusic(winningMusic);
    }

    public void FadeToNewMusic(AudioClip newClip, float fadeDuration = 3f)
    {
        if (audioSource != null && newClip != null)
        {
            StartCoroutine(FadeOutAndChangeMusic(newClip, fadeDuration));
        }
        else
        {
            Debug.LogError("AudioSource or new AudioClip is not assigned.");
        }
    }

    private IEnumerator FadeOutAndChangeMusic(AudioClip newClip, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        // Fade out current music
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in new music
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume; // Ensure the final volume is set correctly
    }
}


