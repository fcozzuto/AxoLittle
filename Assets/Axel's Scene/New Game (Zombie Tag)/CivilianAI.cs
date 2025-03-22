using Unity.VisualScripting;
using UnityEngine;

public class CivilianAI : MonoBehaviour
{
    public Civilian RefToCivilian;
    public PlayerTeamManager playerTeam;
    public GameObject player;

    private float distance;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefToCivilian = GetComponent<Civilian>();
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

    }
}
