using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntitiesState
{
    idle,
    walk,
    attack,
    stagger

}

public class Entities : MonoBehaviour
{
    [Header("State Machine")]
    public EntitiesState currentState;

    [Header("Entitie stats")]
    public float maxHealth;
    public float health;  
    public string entitieName;
    public int baseAttack;
    public float movespeed;
    private Vector2 homePosition;

    [Header("Death Effects")]
    public GameObject deathEffect;
    public float deathEffectTimer = 1f;
    public LootTable thisLoot;

    [Header("Death signal")]
    public SignalObserver roomSignal;

    private void Awake()
    {
       health = maxHealth;
       homePosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth;
        currentState = EntitiesState.idle;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if(roomSignal != null)
            {
                roomSignal.Raise();
            }       
            this.gameObject.SetActive(false);       
        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerup();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }



    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectTimer);
        }
    }

    public void Knock(Rigidbody2D entitieRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(entitieRigidbody, knockTime));
        TakeDamage(damage);
    }
    


    private IEnumerator KnockCo(Rigidbody2D entitieRigidbody, float knockTime)
    {
        if (entitieRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            entitieRigidbody.velocity = Vector2.zero;
            currentState = EntitiesState.idle;
            entitieRigidbody.velocity = Vector2.zero;
        }
    }
}
