using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 spawnPosition;

    private void Awake()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            other.transform.position = spawnPosition;
            
        }
    }
}
