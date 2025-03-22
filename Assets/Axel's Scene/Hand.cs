using System;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject ObjectHeld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void Update()
    {
        if (ObjectHeld == null)
            DetectClicks();
        else
            InteractWithObject();
    }

    void DetectClicks()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            Debug.Log("LC detected!");

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("That's a hit! Hit object: " + hit.collider.gameObject.name);

                Grabable grabable = hit.collider.GetComponent<Grabable>();

                if (grabable != null)
                {
                    Debug.Log("Grabable detected: " + grabable.gameObject.name);
                    grabable.GrabObject();
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything!");
            }
        }
    }

    // We basically call Interact on the Grabable Object. Grabable is a superclass for all the other items.
    void InteractWithObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectHeld.GetComponent<Grabable>().Interact();
            ObjectHeld = null;
        }
    }

    public Boolean hasEmptyHand()
    {
        if (ObjectHeld != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetObjectInHand(Grabable grabable)
    {
        this.ObjectHeld = grabable.gameObject;
    }

}
