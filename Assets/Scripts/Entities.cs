using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntitiesState
{
    idle,
    walk,
    attack,
    stagger
}

public class Entities : MonoBehaviour
{
    public EntitiesState currentState;
    public int health;
    public string entitieName;
    public int baseAttack;
    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
