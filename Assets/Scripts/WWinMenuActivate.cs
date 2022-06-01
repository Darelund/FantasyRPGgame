using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WWinMenuActivate : MonoBehaviour
{
    public GameObject character;
    public GameObject gamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(character.activeInHierarchy == false)
        {
            gamePanel.SetActive(true);
        }
    }
}
