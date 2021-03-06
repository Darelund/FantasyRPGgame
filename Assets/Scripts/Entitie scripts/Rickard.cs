using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rickard : Log
{
   
    public HealthBar healthBar;

    public override void Start()
    {
        currentState = EntitiesState.idle;
        logRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        healthBar.SetMaxhealth(maxHealth);
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.SetHealth(health);
        if (health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
        }
    }


    public override void CheckDistance()
    {
        // if chase radius is more or equal to the distance between target and object with this script then run if
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EntitiesState.idle || currentState == EntitiesState.walk && currentState != EntitiesState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);

                ChangeAnim(temp - transform.position);
                logRigidbody.MovePosition(temp);
                ChangeState(EntitiesState.walk);

            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EntitiesState.walk && currentState != EntitiesState.stagger)
            {
                StartCoroutine(AttackCo());
               
            }

        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EntitiesState.attack;
        anim.SetBool("attack", true);
       
        yield return new WaitForSeconds(0.5f);
        currentState = EntitiesState.walk;
        anim.SetBool("attack", false);
    }


   
}
