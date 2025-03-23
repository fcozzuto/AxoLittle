using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public GameObject FollowPoint;
    public GameObject FollowPointL;
    public GameObject FollowPointR;

    public AudioSource AudioSource;
    public AudioClip FootstepStart;
    public AudioSource FootstepLoop;
    public AudioClip FootstepEnd;

    public bool WasWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get movement input from player
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        float moveY = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        moveInput = new Vector2(moveX, moveY).normalized; // Normalize to prevent diagonal speed boost
        if(moveX != 0 || moveY != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(50 * Time.time));
            WasWalking = true;
            if(!WasWalking)
            {
                AudioSource.PlayClipAtPoint(FootstepStart, transform.position);
            }
            else if (FootstepLoop.loop == false)
            {
                FootstepLoop.loop = true;
                FootstepLoop.Play();
            }
        }
        else if (WasWalking)
        {
            WasWalking = false;
            FootstepLoop.loop = false;
            FootstepLoop.Stop();
            AudioSource.PlayClipAtPoint(FootstepEnd, transform.position);
        }

        if (moveX == 1)
            FollowPoint.transform.position = FollowPointL.transform.position;
        else if (moveX == -1)
            FollowPoint.transform.position = FollowPointR.transform.position;

    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        rb.linearVelocity = moveInput * moveSpeed;
    }
}

