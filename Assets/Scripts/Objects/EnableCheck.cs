using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCheck : MonoBehaviour
{
    public GameObject thing;
    public GameObject thisGameobject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!thing.activeInHierarchy)
        {
            thisGameobject.SetActive(false);
        }
    }
}
