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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefToCivilian = GetComponent<Civilian>();
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetY = Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
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

    private void RandomMove()
    {
        float x = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetX, 0) * 2 - 1; // Smooth X movement
        float y = Mathf.PerlinNoise(Time.time * 0.5f + noiseOffsetY, 0) * 2 - 1; // Smooth Y movement

        Vector2 direction = new Vector2(x, y).normalized; // Normalize so speed stays consistent
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
