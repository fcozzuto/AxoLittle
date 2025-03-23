using UnityEngine;
using UnityEngine.InputSystem;

public class CustomPlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;

    private PlayerInputManager playerInputManager;
    private int playerIndex = 0;

    private void Awake()
    {

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerIndex++;
        if (playerIndex == 0)
        {
            playerInputManager.playerPrefab = playerPrefab1;
        } 
        
        if (playerIndex == 1)
        {
            playerInputManager.playerPrefab = playerPrefab2;
        }
    }
}
