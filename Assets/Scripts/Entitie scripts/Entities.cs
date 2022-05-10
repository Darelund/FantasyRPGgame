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
    public EntitiesState currentState;
    public float maxHealth;
    public float health;
    public string entitieName;
    public int baseAttack;
    public float movespeed;

    private void Awake()
    {
       maxHealth = health;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
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
