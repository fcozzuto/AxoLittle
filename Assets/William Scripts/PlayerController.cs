using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    Vector2 move;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move * Time.deltaTime * playerSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + move);
    }
}
