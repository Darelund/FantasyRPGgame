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
    public int health;
    public string entitieName;
    public int baseAttack;
    public float movespeed;

    public void Knock(Rigidbody2D entitieRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(entitieRigidbody, knockTime));
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
