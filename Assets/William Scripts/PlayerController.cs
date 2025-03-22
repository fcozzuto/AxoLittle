using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] int playerIndex = 0;

    Vector2 move;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move * Time.deltaTime * playerSpeed);
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + move);
    }
}
