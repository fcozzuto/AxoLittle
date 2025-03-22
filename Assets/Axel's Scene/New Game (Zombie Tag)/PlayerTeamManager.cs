using UnityEngine;

public class PlayerTeamManager : MonoBehaviour
{
    public string Team; // Can have two values: "BU" or "UDS"
    public float ConversionSpeed = 1 / 60;
    public float ConversionRadius = 1f;

    public Civilian[] civilians;
    public GameObject followPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        civilians = FindObjectsOfType<Civilian>(); // Get all GameObjects with Civilian script

        // Debugging: Print the names of all found civilians
        foreach (var civilian in civilians)
        {
            Debug.Log("Found Civilian: " + civilian.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
