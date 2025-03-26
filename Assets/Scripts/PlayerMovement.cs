using UnityEngine;
using UnityEngine.InputSystem;

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
        moveInput = new Vector2(moveInput.x, moveInput.y).normalized;
        if(moveInput.x != 0 || moveInput.y != 0)
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

        if (moveInput.x == 1)
            FollowPoint.transform.position = FollowPointL.transform.position;
        else if (moveInput.x == -1)
            FollowPoint.transform.position = FollowPointR.transform.position;


    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }
}

