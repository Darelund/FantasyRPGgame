using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;

    [Header("Activate Inventory")]
    private bool isActive;
    public GameObject inventoryActivePanel;


    

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("pause"))
        {
            ChangePause();
       
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeInventory();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void ChangeInventory()
    {
        isActive = !isActive;
        if (isActive)
        {
            inventoryActivePanel.SetActive(true);
           
        }
        else
        {
            inventoryActivePanel.SetActive(false);
           
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
