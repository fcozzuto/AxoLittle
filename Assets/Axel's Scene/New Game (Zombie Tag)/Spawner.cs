using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;  // Assign the prefab in the Inspector
    public GameObject BomberPrefab;  // Assign the prefab in the Inspector
    public GameObject CurlingPrefab;  // Assign the prefab in the Inspector
    public float spawnInterval = 2f;  // Time between spawns
    public Vector2 minBounds = new Vector2(-5f, -5f);  // Bottom-left corner
    public Vector2 maxBounds = new Vector2(5f, 5f);    // Top-right corner

    public PlayerTeamManager[] playerTeamManagers;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
        playerTeamManagers = FindObjectsOfType<PlayerTeamManager>();
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn == null) return; // Prevent errors if no prefab is assigned

        Vector2 randomPosition = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );

        Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        foreach (var playerTeamManager in playerTeamManagers)
        {
            playerTeamManager.RecheckCivilians();
        }
    }
}

