using UnityEngine;

public class CivilianAI : MonoBehaviour
{
    public Civilian RefToCivilian;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefToCivilian = GetComponent<Civilian>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
