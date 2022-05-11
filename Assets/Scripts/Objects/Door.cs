using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    key,
    enemy,
    button,
    basementKey

}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }




    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                // Does the player have a key?
                if(playerInventory.numberOfKeys > 0)
                {
                    // Remove a player key
                    playerInventory.numberOfKeys--;
                    // If so, call the open method
                    Open();
                }
               else if (playerInventory.numberOfBasementKeys > 0)
                {
                    // Remove a player key
                    playerInventory.numberOfBasementKeys--;
                    // If so, call the open method
                    Open();
                }
                

            }
        }
    }

    public void Open()
    {
        // Turn off the doors sprite renderer
        doorSprite.enabled = false;
        // Set open to true
        open = true;
        // Turn off the box collider
        physicsCollider.enabled = false;
    }

    public void Close()
    {

    }
}
