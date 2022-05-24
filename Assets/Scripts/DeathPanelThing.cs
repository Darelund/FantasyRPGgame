using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelThing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        SceneManager.LoadScene("OpeningCutscene");
    }

    // Need to be added
    public void LoadGame()
    {

    }

    // Need to be added
    public void Options()
    {

    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
