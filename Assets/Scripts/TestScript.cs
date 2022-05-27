using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // A public variable with the type float and name "speed" with the value of 5f
    public float speed = 5f;

    // Start is called before the first frame update
    public void Start()
    {
        
    }
    // Update is called once per frame
   public  void Update()
    {
        // Will find the gameobject that is attached to this script, find its transform and then it will translate its x, y and z axis by some values
        transform.Translate(speed, 0, 0);
    } 
}
