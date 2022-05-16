using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum DoorType
{
    key,
    enemy,
    button,
    basementKey,
    doorSwitch,
    seaKey,
    castleKey

}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    

    [Header("Signals and dialogs")] 
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public BoxCollider2D triggerCollider;

    private void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
    }




    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerInRange && thisDoorType == DoorType.key || thisDoorType == DoorType.button || thisDoorType == DoorType.doorSwitch || thisDoorType == DoorType.basementKey || thisDoorType == DoorType.enemy || thisDoorType == DoorType.castleKey || thisDoorType == DoorType.seaKey)
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
                
                else
                {
                    if (dialogBox.activeInHierarchy)
                    {
                        dialogBox.SetActive(false);
                    }
                    else
                    {
                        dialogBox.SetActive(true);
                        dialogText.text = dialog;
                    }
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

        triggerCollider.enabled = false;
    }

    public void Close()
    {

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("Player no longer in range");
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
