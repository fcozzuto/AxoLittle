using UnityEngine;

public class Civilian : MonoBehaviour
{
    public string Team;
    public float ConversionRateTowardsBU = 0f;
    public float ConversionRateTowardsUDS = 0f;
    public float Loyalty = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Team = null;
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
            Debug.Log("Switched to the BU Team");
        }

        else if (ConversionRateTowardsUDS > 1f)
        {
            Team = "UDS";
            ConversionRateTowardsBU = 0;
            Debug.Log("Switched to the UDS Team");
        }
    }
}
