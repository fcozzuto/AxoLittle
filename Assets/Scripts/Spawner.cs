using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;  // Assign the prefab in the Inspector
    public GameObject BomberPrefab;  // Assign the prefab in the Inspector
    public GameObject CurlingPrefab;  // Assign the prefab in the Inspector

    public float RegularProportion = 0.8f;
    public float BomberProportion = 0.1f;
    public float CurlingProportion = 0.1f;

    public float spawnInterval = 2f;  // Time between spawns
    public Vector2 minBounds = new Vector2(-5f, -5f);  // Bottom-left corner
    public Vector2 maxBounds = new Vector2(5f, 5f);    // Top-right corner

    public PlayerTeamManager[] playerTeamManagers;
    public PlayerMovementAI[] playerMovementAI;
    public GameTimer timer;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
        playerTeamManagers = FindObjectsOfType<PlayerTeamManager>();
        playerMovementAI = FindObjectsOfType<PlayerMovementAI>();
        timer = FindAnyObjectByType<GameTimer>();
    }

    private void SpawnPrefab()
    {
        if (!timer.gameOver)
        {
            if (prefabToSpawn == null) return; // Prevent errors if no prefab is assigned

            Vector2 randomPosition = new Vector2(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y)
            );

            float randomValue = Random.Range(0f, 1f);
            if (randomValue > RegularProportion)
            {
                if (randomValue < RegularProportion + BomberProportion)
                    Instantiate(BomberPrefab, randomPosition, Quaternion.identity);
                else
                    Instantiate(CurlingPrefab, randomPosition, Quaternion.identity);
            }
            else
                Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

            foreach (var playerTeamManager in playerTeamManagers)
            {
                playerTeamManager.RecheckCivilians();
            }
            foreach (var playerMovementAI in playerMovementAI)
            {
                playerMovementAI.RecheckCivilians();
            }
        }
    }
}

