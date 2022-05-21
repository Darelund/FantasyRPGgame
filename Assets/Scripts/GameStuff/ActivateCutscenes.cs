using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCutscenes : MonoBehaviour
{
    public GameObject virtualCamera;
    public GameObject Camera1;
    public GameObject player;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
          
            
            virtualCamera.SetActive(true);
            player.SetActive(false);
            Camera1.SetActive(false);
        }
    }



    public void OnDisable()
    {
        virtualCamera.SetActive(false);
    }


}
