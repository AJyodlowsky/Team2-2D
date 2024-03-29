using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Script started/written by Jay Yodlowsky for SGD-112
//Instructor: George Cox

public class UIController : MonoBehaviour
{


    public void OnClickStart()
    {
        SceneManager.LoadScene("LevelOne");
    }


    public void OnClickQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnClickHELP()
    {
        SceneManager.LoadScene("HELP");
    }


    public void OnClickCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Credits" || currentSceneName == "HELP" || currentSceneName == "WIN")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit button was clicked");
            Application.Quit();
        }
    }
}
