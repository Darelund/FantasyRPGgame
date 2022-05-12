using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EntitiesState.idle || currentState == EntitiesState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);

                ChangeAnim(temp - transform.position);
                logRigidbody.MovePosition(temp);
                ChangeState(EntitiesState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("wakeUp", false);
        }
    }
}
