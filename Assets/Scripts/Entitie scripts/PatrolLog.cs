using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;

    public override void Start()
    {
        base.Start();
    }


    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EntitiesState.idle || currentState == EntitiesState.walk && currentState != EntitiesState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                logRigidbody.MovePosition(temp);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > float.Epsilon)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, movespeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                logRigidbody.MovePosition(temp);

                ChangeAnim(temp - transform.position);
                logRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
