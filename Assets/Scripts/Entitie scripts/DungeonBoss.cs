using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBoss : Entities
{
    public Rigidbody2D logRigidbody;

    [Header("Target variables")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    [Header("Projectiles")]
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    [Header("Animator")]
    public Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentState = EntitiesState.idle;
        logRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("wakeUp", true);
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
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Find a distance between a log and the target
    public virtual void CheckDistance()
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
                anim.SetBool("wakeUp", true);

                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire = false;
                    ChangeState(EntitiesState.walk);
                    anim.SetBool("wakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    public void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    public void ChangeState(EntitiesState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}

