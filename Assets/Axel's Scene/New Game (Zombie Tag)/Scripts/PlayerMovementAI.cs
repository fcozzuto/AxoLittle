using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementAI : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public GameObject FollowPoint;
    public GameObject FollowPointL;
    public GameObject FollowPointR;

    private Civilian[] civilians;

    float lastX = 0f;
    float lastY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        RecheckCivilians(); // Initialize the civilians list at start
    }

    void Update()
    {
        // Get movement input from player
        float moveX = this.transform.position.x - lastX;
        float moveY = this.transform.position.y - lastY;

        lastX = this.transform.position.x;
        lastY = this.transform.position.y;

        if (moveX != 0 || moveY != 0)
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(50 * Time.time));

        if (moveX > 0)
            FollowPoint.transform.position = FollowPointL.transform.position;
        else if (moveX < 0)
            FollowPoint.transform.position = FollowPointR.transform.position;

        // Find closest enemy civilian
        float minDist = float.MaxValue;
        Civilian targetCivilian = null;

        foreach (var civilian in civilians)
        {
            if (civilian.Team != GetComponent<PlayerTeamManager>().Team) // Find enemy team
            {
                float distance = Vector2.Distance(transform.position, civilian.transform.position);
                if (distance < minDist)
                {
                    minDist = distance;
                    targetCivilian = civilian;
                }
            }
        }

        if (targetCivilian != null)
            FollowEnemy(targetCivilian);
        else
            Debug.Log("Target civilian not found");
    }

    private void FollowEnemy(Civilian specialcivilian)
    {
        GameObject civilianPos = specialcivilian.gameObject;
        float distance = Vector2.Distance(transform.position, civilianPos.transform.position);
        Vector2 direction = civilianPos.transform.position - transform.position;

        Debug.Log("Moving towards target");

        transform.position = Vector2.MoveTowards(this.transform.position, civilianPos.transform.position, moveSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D velocity
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void RecheckCivilians()
    {
        Debug.Log("Recheck Civilians");
        civilians = FindObjectsOfType<Civilian>(); // Get all GameObjects with Civilian script
    }
}

