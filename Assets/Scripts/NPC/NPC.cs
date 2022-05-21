using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCState
{
    idle,
    walk,
    attack,
    stagger
}

public class NPC : MonoBehaviour
{
    public NPCState currentState;
    public int health;
    public string npcName;
    public float movespeed;

   
}
