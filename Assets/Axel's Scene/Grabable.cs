using System;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private GameObject HandReference;
    private Hand HandScript;
    public Boolean isGrabbed = false;

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
            isGrabbed = true;
            HandScript.SetObjectInHand(this);
        }
        else
        {
            Debug.Log("Hand is not empty, cannot grab object.");
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        this.transform.SetParent(null);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)this.transform.position).normalized;

        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;

        if (rb != null)
        {
            rb.AddForce(direction * 10f, ForceMode2D.Impulse);
        }
    }
}


