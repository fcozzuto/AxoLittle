using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip HeavyClick;

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }
    public void PlayMultiGame()
    {
        StartCoroutine(StartMultiGame());
    }
    public void OpenCreditsScene()
    {
        StartCoroutine(OpenCredits());
    }

    private IEnumerator StartGame()
    {
        AudioSource.PlayOneShot(HeavyClick);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("AxelScene");
    }

    private IEnumerator StartMultiGame()
    {
        AudioSource.PlayOneShot(HeavyClick);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("WilliamScene");
    }

    public IEnumerator OpenCredits()
    {
        AudioSource.PlayOneShot(HeavyClick);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ScrollingCredits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   
