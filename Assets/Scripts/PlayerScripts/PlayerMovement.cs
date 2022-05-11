using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 5f;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator playerAnimator;
    public FloatValue currentHealth;
    public SignalObserver playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public SignalObserver playerHit;
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        playerAnimator = GetComponent<Animator>(); 
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator.SetFloat("moveX", 0);
        playerAnimator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Is the player in an interaction
        if(currentState == PlayerState.interact)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
      
        // if space and currentstate is not attack and stagger, play attack animation
        if(Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }

        // If character is on walk or idle play animation and walk method
       else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }                   
    }

    // Attack animation method
    private IEnumerator AttackCo()
    {
        playerAnimator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        playerAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
     
    }

    public void RaiseItem()
    {
        if(playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                playerAnimator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                playerAnimator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;             
                playerInventory.currentItem = null;
            }
        }        
    }


    // Method to update character animation
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            playerAnimator.SetFloat("moveX", change.x);
            playerAnimator.SetFloat("moveY", change.y);
            playerAnimator.SetBool("moving", true);
        }
        else
        {
            playerAnimator.SetBool("moving", false);
        }
    }
    // Move character method
    void MoveCharacter()
    {
        playerRigidbody.MovePosition(transform.position + change.normalized * Time.fixedDeltaTime * speed);
    }

    // Coroutine for knocktime
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RunTimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RunTimeValue > 0)
        {       
            StartCoroutine(KnockCo(knockTime));
        }     
       else
        {
            this.gameObject.SetActive(false);
        }
    }

    // Knocktime 
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (playerRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidbody.velocity = Vector2.zero;
        }
    }
}
