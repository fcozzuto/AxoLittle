using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    private SpecialCivilian[] specialCivilians;
    private List<SpecialCivilian> SpecialCiviliansBU = new List<SpecialCivilian>();
    private List<SpecialCivilian> SpecialCiviliansUDS = new List<SpecialCivilian>();

    public GameObject ParticlesEnd;
    public TextMeshProUGUI quitText;
    private bool isGameOver = false;

    void Start()
    {
        specialCivilians = FindObjectsOfType<SpecialCivilian>();

        foreach (var civilian in specialCivilians)
        {
            if (civilian.Team == "BU")
                SpecialCiviliansBU.Add(civilian);
            else if (civilian.Team == "UDS")
                SpecialCiviliansUDS.Add(civilian);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            // If Escape is pressed, load the "BasicMenu" scene
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("BasicMenu");
            }
            return; // Prevent unnecessary updates once the game is over
        }


        bool areAllBUDead = true;
        bool areAllUDSDead = true;

        // Check if the lists are empty
        if (SpecialCiviliansBU.Count == 0 || SpecialCiviliansUDS.Count == 0)
        {
            EndGame();
            return;
        }

        foreach (var civilian in SpecialCiviliansBU)
        {
            if (civilian.gameObject.activeSelf)
            {
                areAllBUDead = false;
                break;
            }
        }

        foreach (var civilian in SpecialCiviliansUDS)
        {
            if (civilian.gameObject.activeSelf)
            {
                areAllUDSDead = false;
                break;
            }
        }

        // Fix operator precedence issue
        if ((areAllBUDead || areAllUDSDead) && !isGameOver)
            EndGame();
    }

    private void EndGame()
    {
        isGameOver = true;
        Instantiate(ParticlesEnd, transform.position, Quaternion.identity);
        quitText.gameObject.SetActive(true);
        Debug.Log("Game is over!");
    }
}

