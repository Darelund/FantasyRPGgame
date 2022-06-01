using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrueBoss : Log
{
    public HealthBar healthBar;
    public float attackWait = 0.5f;
    private bool isDone = false;


    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private float timer = 20f;
    private float timer2 = 1f;
    public override void Start()
    {
        currentState = EntitiesState.idle;
        logRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        healthBar.SetMaxhealth(maxHealth);
    }
    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;

            fireDelaySeconds = fireDelay;
        }
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
                if(health < 50)
                {
                    if(timer > timer2)
                    {
                        if (canFire)
                        {
                           
                            Vector3 tempVector = target.transform.position - transform.position;
                            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                            current.GetComponent<Projectile>().Launch(tempVector);
                            canFire = false;
                            ChangeState(EntitiesState.walk);
                            timer2++;

                            if (!isDone)
                            {
                                StartCoroutine(SuperAttackCo());
                                isDone = true;
                            }
                        }
                    }                  
                   

                }

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

    public IEnumerator SuperAttackCo()
    {
        movespeed = 0;
        currentState = EntitiesState.animeRage;
        anim.SetBool("superAttack", true);

        yield return new WaitForSeconds(attackWait);
        movespeed = 2;
        currentState = EntitiesState.walk;
        anim.SetBool("superAttack", false);
           
    }
}
