using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ObjectsToActivateT3;
    public GameObject ObjectsToActivateT3Point5;

    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip StartCDSound; // The initial background music (before win)
    void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        AudioSource.PlayClipAtPoint(StartCDSound, transform.position);

        yield return new WaitForSeconds(3f);
        ObjectsToActivateT3.SetActive(true);
        ObjectsToActivateT3Point5.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
