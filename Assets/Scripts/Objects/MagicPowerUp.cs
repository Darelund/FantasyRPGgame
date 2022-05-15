using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerUp : PowerUp
{
    public Inventory playerInventory;
    public float magicValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            playerInventory.currentMagic += magicValue;
            if (playerInventory.currentMagic > playerInventory.maxMagic)
            {
                playerInventory.currentMagic = playerInventory.maxMagic;
            }

            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
