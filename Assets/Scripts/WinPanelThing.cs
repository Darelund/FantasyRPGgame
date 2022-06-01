using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelThing : MonoBehaviour
{
    private PlayerMovement player;
    private FloatValue value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChallengeGod()
    {


        SceneManager.LoadScene("Godbattle");

    }

    // Need to be added
    public void LoadGame()
    {

    }

    // Need to be added
    public void Options()
    {

    }

    public void GameFinished()
    {
        SceneManager.LoadScene("GameFinished");
    }

    public void GameTurnOff()
    {
        Application.Quit();
    }
}
