using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void buttonPlayAgain()
    {
        //Load the game scene
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        SceneManager.LoadScene("Level_1");
    }

    public void buttonMainMenu()
    {
        //Load the main menu scene
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void buttonQuit()
    {
        //Exit the game
        exitGame();
    }

    private void exitGame()
    {
        //Exit the game
        Application.Quit();
    }
}
