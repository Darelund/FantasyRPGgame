using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals and dialogs")]
    public SignalObserver raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RunTimeValue;
        if(isOpen)
        {
            anim.SetBool("opened", true);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                // Open chest
                OpenChest();
            }
            else
            {
                // Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog window on
        dialogBox.SetActive(true);
        // Dialog text = content text
        dialogText.text = contents.itemDescription;
        // Add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Raise the signal to the player to animate
        raiseItem.Raise();       
        // Raise the context clue
        context.Raise();
        // Set the chest anime to opened
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.RunTimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
       
        
            // Dialog off
            dialogBox.SetActive(false);
          
            // Raise the signal to the player to stop animating
            raiseItem.Raise();
            
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            Debug.Log("Player in range");
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            Debug.Log("Player no longer in range");
            context.Raise();
            playerInRange = false;
        }
    }

}
