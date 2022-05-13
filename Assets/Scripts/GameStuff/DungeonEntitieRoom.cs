using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntitieRoom : DungeonRoom
{
    public Door[] doors;
 

    public void CheckEnemies()
    {
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].gameObject.activeInHierarchy && i < entities.Length - 1)
            {
                // Check entities active
                return;
            }
        }
        OpenDoors();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
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
            CloseDoors();
            virtualCamera.SetActive(true);
        }
     
    }

    public override void OnTriggerExit2D(Collider2D other)
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
    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }



}
