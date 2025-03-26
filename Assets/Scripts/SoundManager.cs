using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip loopClip; // The initial background music (before win)
    public AudioClip AxolotlWinningMusic; // The temporary sound before looping again
    public AudioClip AxolotlLoopedWinningMusic; // The final music that will loop after winning
    public AudioClip FrogWinningMusic; // The temporary sound before looping again
    public AudioClip FrogLoopedWinningMusic; // The final music that will loop after winning

    void Start()
    {
        if (audioSource != null && loopClip != null)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true; // Enable looping
            audioSource.Play(); // Start playing the initial music
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned.");
        }
    }

    public void TriggerWin(string Team)
    {
        if(Team == "Axolotl")
            FadeToNewMusic(AxolotlWinningMusic, AxolotlLoopedWinningMusic);
        if (Team == "Frog")
            FadeToNewMusic(FrogWinningMusic, FrogLoopedWinningMusic);
    }

    public void FadeToNewMusic(AudioClip newClip, AudioClip nextLoopClip, float fadeDuration = 3f)
    {
        if (audioSource != null && newClip != null && nextLoopClip != null)
        {
            StartCoroutine(FadeOutAndPlayNewMusic(newClip, nextLoopClip, fadeDuration));
        }
        else
        {
            Debug.LogError("AudioSource or new AudioClip is not assigned.");
        }
    }

    private IEnumerator FadeOutAndPlayNewMusic(AudioClip newClip, AudioClip nextLoopClip, float fadeDuration)
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
        audioSource.loop = false; // Don't loop the temporary music
        audioSource.Play();

        // Fade in new music
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume; // Ensure the final volume is set correctly

        // Wait for the new clip to finish
        yield return new WaitForSeconds(newClip.length);

        // Start the final looping music
        audioSource.clip = nextLoopClip;
        audioSource.loop = true; // Loop the final background music
        audioSource.Play();
    }
}



