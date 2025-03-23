using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float endTime = 60f;
    private float currentTime;
    public GameObject endScreen;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI Team1Score;
    public TextMeshProUGUI Team2Score;

    public GameObject posLeft;
    public GameObject posRight;

    public GameObject Team1Prefab;
    public GameObject Team2Prefab;

    public Civilian[] civilians;

    public bool gameOver = false;
    public bool hasSpawned = false;

    void Start()
    {
        currentTime = endTime;
        timer.text = currentTime.ToString();
        StartCoroutine(StopAfterTimer());
    }

    void Update()
    {
        // Only update the timer if the game is not over
        if (!gameOver)
        {
            currentTime -= Time.deltaTime;
            int num = Mathf.CeilToInt(currentTime); // Round up the seconds
            timer.text = num.ToString();

            if (currentTime <= 0)
            {
                // Timer reaches zero, mark game as over
                gameOver = true;
                StopCoroutine(StopAfterTimer()); // Ensure it's stopped immediately if the timer ends before the delay
                CalculateTeamScore(); // Make sure score is calculated when timer ends
            }
        }
    }

    private IEnumerator StopAfterTimer()
    {
        // Wait for the timer to expire
        yield return new WaitForSeconds(endTime);

        // This block will be called after the timer ends
        endScreen.SetActive(true);
    }

    private void CalculateTeamScore()
    {
        // Ensure this only happens when the game is over and spawn has not been triggered yet
        if (gameOver && !hasSpawned)
        {
            civilians = FindObjectsOfType<Civilian>(); // Get all civilians
            int team1Amount = 0;
            int team2Amount = 0;

            // Count civilians per team
            foreach (var civ in civilians)
            {
                if (civ.Team == "BU")
                {
                    team1Amount++;
                }
                else if (civ.Team == "UDS")
                {
                    team2Amount++;
                }
            }

            // Display the scores
            Team1Score.text = "Player1 Score : " + team1Amount.ToString();
            Team2Score.text = "Player2 Score : " + team2Amount.ToString();

            Debug.Log("Game is over");

            // Only spawn the teams if they haven't been spawned already
            hasSpawned = true;
            for (int i = 0; i < team1Amount; i++)
            {
                Instantiate(Team1Prefab, posLeft.transform.position, Quaternion.identity);
            }

            for (int i = 0; i < team2Amount; i++)
            {
                Instantiate(Team2Prefab, posRight.transform.position, Quaternion.identity);
            }
        }
    }
}


