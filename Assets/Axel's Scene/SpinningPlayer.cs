using UnityEngine;

public class SpinningPlayer : MonoBehaviour
{
    public float spinningSpeed = 100f; // Speed in degrees per second

    void Update()
    {
        transform.Rotate(0, 0, -spinningSpeed * Time.deltaTime);
    }
}


