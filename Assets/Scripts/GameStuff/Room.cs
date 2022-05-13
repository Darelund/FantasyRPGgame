using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Objects")]
    public Entities[] entities;
    public Breakable[] breakable;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            // activate all objects
            for (int i = 0; i < entities.Length; i++)
            {
                ChangeActivation(entities[i], true);
            }
            for (int i = 0; i < breakable.Length; i++)
            {
                ChangeActivation(breakable[i], true);
            }
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // Deactivate all objects

            for (int i = 0; i < entities.Length; i++)
            {
                ChangeActivation(entities[i], false);
            }
            for (int i = 0; i < breakable.Length; i++)
            {
                ChangeActivation(breakable[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
