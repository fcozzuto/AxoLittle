using System;
using Unity.VisualScripting;
using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefToCivilian = GetComponent<Civilian>();
        noiseOffsetX = UnityEngine.Random.Range(0f, 100f);
        noiseOffsetY = UnityEngine.Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
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
                    EnemyFound = true;
                    FollowEnemy(civilian);
                    break;
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
            civilian.Convert(ConversionSpeed, RefToCivilian.Team);
        }
    }

    private void RandomMove()
    {
        float x = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetX, 0) * 2 - 1; // Smooth X movement
        float y = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetY, 0) * 2 - 1; // Smooth Y movement

        Vector2 direction = new Vector2(x, y).normalized; // Normalize so speed stays consistent
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
