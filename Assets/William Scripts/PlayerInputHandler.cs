
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerController playerController;
    PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindFirstObjectByType<PlayerController>();
        var index = playerInput.playerIndex;
        playerController = GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.OnMove(context);
        }
        else
        {
            Debug.Log("PlayerController is not assigned");
        }

        
    }
}
