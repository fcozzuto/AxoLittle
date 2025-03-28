using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CivilianAI : MonoBehaviour
{
    public Civilian RefToCivilian;
    public PlayerTeamManager playerTeam;
    public GameObject player;

    private float distance;
    public float speed = 3f;

    private float noiseOffsetX;
    private float noiseOffsetY;

    public float ConversionSpeed = 1 / 60;
    public float ConversionRadius = 1f;

    public float chasingThreshold = 5f;

    public float explosionRadius = 10f;

    private bool isCurling = false; // To ensure we don't trigger multiple movements at once

    private bool hasExploded = false;
    public AudioClip explosionSound;
    public AudioClip ahSound;
    public AudioClip wooSound;

    public float chanceToAh = 0.001f;
    public float chanceToWoo = 0.001f;

    public GameTimer timer;
    public Animator animator;

    public GameObject Smoke;

    public string Category; // Can hold different values: "Normal", "Bomber", "Curling"
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefToCivilian = GetComponent<Civilian>();
        noiseOffsetX = UnityEngine.Random.Range(0f, 100f);
        noiseOffsetY = UnityEngine.Random.Range(0f, 100f);

        timer = FindAnyObjectByType<GameTimer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            if (timer.gameOver)
            {
                Destroy(this.gameObject);
            }
        }

        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue < chanceToAh)
            AudioSource.PlayClipAtPoint(ahSound, transform.position);
        else if (randomValue < chanceToWoo + chanceToAh)
            AudioSource.PlayClipAtPoint(wooSound, transform.position);

        // in like the update, when the chars are moving
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(50 * Time.time));
        if (player != null)
        {
            Boolean EnemyFound = false;
            foreach (var civilian in playerTeam.civilians)
            {
                float distanceToCivilian = Vector2.Distance(transform.position, civilian.gameObject.transform.position);
                if (civilian.Team != RefToCivilian.Team && distanceToCivilian < chasingThreshold)
                {
                    if (isCurling)
                    {
                        civilian.Convert(99999999f, RefToCivilian.Team);
                        break;
                    }
                    else
                    {
                        EnemyFound = true;
                        FollowEnemy(civilian);
                        break;
                    }
                }
            }
            if(!EnemyFound)
                FollowPlayer();
        }
        else
        {
            RandomMove();
        }
    }

    public void setFollow(PlayerTeamManager playerToFollow)
    {
        playerTeam = playerToFollow;
        player = playerTeam.followPoint;
    }

    private void FollowPlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FollowEnemy(Civilian civilian)
    {
        GameObject civilianPos = civilian.gameObject;
        distance = Vector2.Distance(transform.position, civilianPos.transform.position);
        Vector2 direction = civilianPos.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, civilianPos.transform.position, speed * Time.deltaTime);

        if (distance < ConversionRadius)
        {
            if (Category == "Normal")
            {
                civilian.Convert(ConversionSpeed, RefToCivilian.Team);
            }
            else if (Category == "Bomber")
            {
                Explode();
            }
            else if (Category == "Curling")
            {
                Curling(civilianPos);
            }
        }
    }

    private void RandomMove()
    {
        float x = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetX, 0) * 2 - 1; // Smooth X movement
        float y = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetY, 0) * 2 - 1; // Smooth Y movement

        Vector2 direction = new Vector2(x, y).normalized; // Normalize so speed stays consistent
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        animator.SetTrigger("Explode");
        // Wait for 3 seconds before exploding
        yield return new WaitForSeconds(3f);

        Instantiate(Smoke, transform.position, Quaternion.identity);

        foreach (var civilian in playerTeam.civilians)
        {
            float distance = Vector2.Distance(transform.position, civilian.gameObject.transform.position);
            if (distance < explosionRadius && civilian.Team != RefToCivilian.Team)
            {
                civilian.Convert(99999999f, RefToCivilian.Team);
                SpriteRenderer spriteRenderer = RefToCivilian.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = RefToCivilian.RegularSprite;  // Assuming "RegularSprite" is a field in Civilian
                    Category = "Normal";
                }
            }
        }
    }


    private void Curling(GameObject target)
    {
        if (isCurling) return; // Prevent starting the movement if already in progress

        isCurling = true;  // Start the curling movement
        StartCoroutine(MoveAndConvert(target));
    }

    private IEnumerator MoveAndConvert(GameObject target)
    {
        // Ensure it turns back after 5 seconds, no matter what
        StartCoroutine(ResetAfterTime(2f));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Propulse(target));

        if (rb != null)
        {
            yield return new WaitForSeconds(2f);

            // Once a wall is hit, stop the movement and handle logic
            Debug.Log("Hit a wall or other object");
            StopCurling();
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on this object.");
        }
    }

    private IEnumerator Propulse(GameObject target)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float forceAmount = 30f; // Increase force for more noticeable movement

        rb.linearVelocity = direction * forceAmount; // Apply force once as velocity

        yield return new WaitForSeconds(2f); // Wait before stopping
        StopCurling(); // Ensure curling stops properly
    }

    // Resets the object after 5 seconds
    private IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        StopCurling();
    }

    // Handles resetting to civilian state
    private void StopCurling()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop movement
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = RefToCivilian.RegularSprite;
        }

        Category = "Normal";
        rb.linearVelocity = Vector2.zero;  // Stops all movement
        isCurling = false;
    }
}
