using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public SignalObserver reduceMagic;
    public GameManagerIsh gameManager;

    [Header("IFrame stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer framesSprite;

    [Header("Projectiles")]
    public GameObject projectile;
    public Item homework;


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
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }

        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if(playerInventory.CheckForItem(homework))
            {
                StartCoroutine(SecondAttackCo());
            }
           
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

    private IEnumerator SecondAttackCo()
    {     
        currentState = PlayerState.attack;
        yield return null;
        MagicProjectile();
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }
    private void MagicProjectile()
    {
        if(playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(playerAnimator.GetFloat("moveX"), playerAnimator.GetFloat("moveY"));
            FireBall magicProjectileLaunch = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<FireBall>();
            magicProjectileLaunch.Setup(temp, ChoosemAGICProjectileDirection());
            playerInventory.ReduceMagic(magicProjectileLaunch.magicCost);
            reduceMagic.Raise();
        }     
    }

    Vector3 ChoosemAGICProjectileDirection()
    {
        float temp = Mathf.Atan2(playerAnimator.GetFloat("moveY"), playerAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
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
            currentHealth.RunTimeValue = currentHealth.initialValue;
            SceneManager.LoadScene("DeathScene");
            
        }
    }

    // Knocktime 
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (playerRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            framesSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            framesSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
       PlayerData data = SaveSystem.LoadPlayer();

        playerInventory = data.playerInventory;
        currentHealth = data.health;
       

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

}
