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

    private IEnumerator StartGame()
    {
        AudioSource.PlayOneShot(HeavyClick);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("AxelScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   
