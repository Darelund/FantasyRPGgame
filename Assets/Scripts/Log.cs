using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Entities
{
    private Rigidbody2D logRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EntitiesState.idle;
        logRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Find a distance between a log and the target
    void CheckDistance()
    {
        // if chase radius is more or equal to the distance between target and object with this script then run if
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);

            logRigidbody.MovePosition(temp);
            ChangeState(EntitiesState.walk);
        }
    }

    private void ChangeState(EntitiesState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
