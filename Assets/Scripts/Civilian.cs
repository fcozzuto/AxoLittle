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

    public Sprite RegularSprite;  // Assign this in the Inspector or via code

    public GameTimer timer;

    public AudioSource audioSource;
    public AudioClip ConversionSound;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Team = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        civilianAI = GetComponent<CivilianAI>();
        playerTeamManagers = FindObjectsOfType<PlayerTeamManager>();
        timer = FindAnyObjectByType<GameTimer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer != null)
        {
            if (timer.gameOver)
            {
                this.gameObject.SetActive(false);
            }
        }
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
            spriteRenderer.color = new Color(154/255f, 189/255f, 60/255f); // RGB(88, 44, 131)
            Debug.Log("Switched to the BU Team");
            AudioSource.PlayClipAtPoint(ConversionSound, transform.position);
            animator.SetTrigger("Infect");
        }

        else if (ConversionRateTowardsUDS > 1f)
        {
            Team = "UDS";
            spriteRenderer.color = new Color(239/255f, 184/255f, 181/255f); // RGB(72, 106, 92)
            Debug.Log("Switched to the UDS Team");
            AudioSource.PlayClipAtPoint(ConversionSound, transform.position);
            animator.SetTrigger("Infect");
        }

        foreach (var player in playerTeamManagers)
        {
            if(player.Team == Team)
                civilianAI.setFollow(player);
        }
    }
}
