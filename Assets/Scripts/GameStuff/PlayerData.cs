using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public Inventory playerInventory;
  
    public FloatValue health;
    public float[] position;

    public PlayerData(PlayerMovement player)
    {
        playerInventory = player.playerInventory;
       
        health = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;


    }



}
