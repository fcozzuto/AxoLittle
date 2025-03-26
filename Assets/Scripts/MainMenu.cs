using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SinglePlayer()
    {
        SceneManager.LoadScene("AxelScene");
    }

    public void MultiPlayer()
    {
        SceneManager.LoadScene("WilliamScene");
    }
    public void OpenCreditsScene()
    {
        SceneManager.LoadScene("ScrollingCredits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   
