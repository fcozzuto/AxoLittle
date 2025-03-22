using UnityEngine;

public class Civilian : MonoBehaviour
{
    public string Team;
    public float ConversionRateTowardsBU = 0f;
    public float ConversionRateTowardsUDS = 0f;
    public float Loyalty = 0.1f;

    private SpriteRenderer spriteRenderer;
    private CivilianAI civilianAI;
    private PlayerTeamManager[] playerTeamManagers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Team = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        civilianAI = GetComponent<CivilianAI>();
        playerTeamManagers = FindObjectsOfType<PlayerTeamManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Convert(float ConversionSpeed, string TeamOfConversion)
    {
        if (Team == null)
        {
            if (TeamOfConversion == "BU")
            {
                ConversionRateTowardsBU += ConversionSpeed;
                if (ConversionRateTowardsBU >= 1f)
                {
                    ConversionRateTowardsUDS = 0;
                }
            }
            else if (TeamOfConversion == "UDS")
            {
                ConversionRateTowardsUDS += ConversionSpeed;
                if (ConversionRateTowardsUDS >= 1f)
                {
                    ConversionRateTowardsBU = 0;
                }
            }
            else
            {
                Debug.Log("Incorrect Team name!");
            }

            CheckForTeam();
        }
        else
        {
            if (TeamOfConversion != Team)
            {
                if (TeamOfConversion == "BU")
                {
                    ConversionRateTowardsBU += ConversionSpeed * Loyalty;
                    if (ConversionRateTowardsBU >= 1f)
                    {
                        ConversionRateTowardsUDS = 0;
                    }
                }
                else if (TeamOfConversion == "UDS")
                {
                    ConversionRateTowardsUDS += ConversionSpeed * Loyalty;
                    if (ConversionRateTowardsUDS >= 1f)
                    {
                        ConversionRateTowardsBU = 0;
                    }
                }
                else
                {
                    Debug.Log("Incorrect Team name!");
                }

                CheckForTeam();
            }
        }
    }

    public void CheckForTeam()
    {
        if (ConversionRateTowardsBU > 1f)
        {
            Team = "BU";
            spriteRenderer.color = new Color(0.345f, 0.173f, 0.514f); // RGB(88, 44, 131)
            Debug.Log("Switched to the BU Team");
        }

        else if (ConversionRateTowardsUDS > 1f)
        {
            Team = "UDS";
            spriteRenderer.color = new Color(0.282f, 0.416f, 0.361f); // RGB(72, 106, 92)
            Debug.Log("Switched to the UDS Team");
        }

        // Debugging: Print the names of all found civilians
        foreach (var player in playerTeamManagers)
        {
            if(player.Team == Team)
                civilianAI.setFollow(player);
        }
    }
}
