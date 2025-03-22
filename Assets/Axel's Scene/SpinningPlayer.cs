using UnityEngine;
using System.Collections;

public class SpinningPlayer : MonoBehaviour
{
    public float spinningSpeed = 100f; // Speed in degrees per second
    public float lowerPos = 1.0f;
    public float upperPos = 1.3f;
    public float totalBounceSpeed = 1.0f; // Time to complete one full bounce (up and down)
    public float timeToSlowDown = 0.5f; // Time in seconds to slow down before reaching peak

    private bool isBouncing = false; // To prevent starting multiple coroutines

    void Start()
    {
        // Start the bouncing coroutine at the start
        StartCoroutine(Bounce());
    }

    void Update()
    {
        // Handle spinning
        transform.Rotate(0, 0, -spinningSpeed * Time.deltaTime);
    }

    // Coroutine to handle bouncing up and down
    IEnumerator Bounce()
    {
        while (true) // Keep bouncing indefinitely
        {
            // Move from lowerPos to upperPos
            float timeElapsed = 0f;
            while (timeElapsed < totalBounceSpeed / 2f) // Half the total time for the upward movement
            {
                float t = timeElapsed / (totalBounceSpeed / 2f);
                float easedT;

                // Accelerate in the first 'timeToSlowDown' seconds of upward movement (ease-in)
                if (timeElapsed < timeToSlowDown)
                {
                    easedT = Mathf.Sqrt(t); // Ease-in: Accelerate as it moves up
                }
                else
                {
                    easedT = t; // Continue normal movement after acceleration
                }

                transform.position = new Vector3(transform.position.x, Mathf.Lerp(lowerPos, upperPos, easedT), transform.position.z);

                timeElapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // After reaching upperPos, wait a moment before moving down
            timeElapsed = 0f;
            while (timeElapsed < totalBounceSpeed / 2f) // Half the total time for the downward movement
            {
                float t = timeElapsed / (totalBounceSpeed / 2f);
                float easedT;

                // Slow down in the first 'timeToSlowDown' seconds of downward movement (ease-out)
                if (timeElapsed < timeToSlowDown)
                {
                    easedT = Mathf.Pow(t, 2); // Ease-out: Slow down as it nears the bottom
                }
                else
                {
                    easedT = t; // Continue normal movement after deceleration
                }

                transform.position = new Vector3(transform.position.x, Mathf.Lerp(upperPos, lowerPos, easedT), transform.position.z);

                timeElapsed += Time.deltaTime;
                yield return null; // Wait for the next frame
            }
        }
    }
}




