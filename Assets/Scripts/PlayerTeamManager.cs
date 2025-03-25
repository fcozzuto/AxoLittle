using UnityEngine;

public class PlayerTeamManager : MonoBehaviour
{
    public string Team; // Can have two values: "BU" or "UDS"
    public float ConversionSpeed = 1 / 60;
    public float ConversionRadius = 1f;

    public Civilian[] civilians;
    public GameObject followPoint;
    public GameTimer timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        civilians = FindObjectsOfType<Civilian>(); // Get all GameObjects with Civilian script
        timer = FindAnyObjectByType<GameTimer>();

        // Debugging: Print the names of all found civilians
        foreach (var civilian in civilians)
        {
            Debug.Log("Found Civilian: " + civilian.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            if (timer.gameOver)
            {
                this.gameObject.SetActive(false);
            }
        }

        foreach (var civilian in civilians)
        {
            float distance = Vector2.Distance(this.transform.position, civilian.transform.position);
            if (distance < ConversionRadius)
            {
                Debug.Log("Civilian in range of conversion");
                civilian.Convert(ConversionSpeed, Team);
            }
        }
    }

    public void RecheckCivilians()
    {
        civilians = FindObjectsOfType<Civilian>(); // Get all GameObjects with Civilian script
    }
}
