using UnityEngine;

public class Grabable : MonoBehaviour
{
    private GameObject HandReference;
    private Hand HandScript;

    void Start()
    {
        HandReference = GameObject.FindGameObjectWithTag("Hand");

        if (HandReference != null)
        {
            HandScript = HandReference.GetComponent<Hand>();
        }
        else
        {
            Debug.LogWarning("No GameObject with the tag 'Hand' found in the scene.");
        }
    }

    public void GrabObject()
    {
        if (HandScript != null && HandScript.hasEmptyHand())
        {
            // The hand is empty and can grab the object
            Debug.Log("Object grabbed!");
            HandScript.SetObjectInHand(this);
        }
        else
        {
            Debug.Log("Hand is not empty, cannot grab object.");
        }
    }
}


