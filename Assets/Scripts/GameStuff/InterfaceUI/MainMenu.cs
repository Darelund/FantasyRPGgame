using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject sike;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("TrainingScene");
    }

    // Need to be added
    public void LoadGame()
    {
        sike.SetActive(true);
    }

    // Need to be added
    public void Options()
    {
     
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }


    public void GoBack()
    {
        sike.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
