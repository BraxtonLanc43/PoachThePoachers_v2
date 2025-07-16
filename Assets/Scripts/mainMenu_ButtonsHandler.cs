using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu_ButtonsHandler : MonoBehaviour
{
    //Vars
    public Button btnSound;
    public Sprite sprSoundOff;
    public Sprite sprSoundOn;
    private int highScore;
    private int highScore_Gems;
    private int highScore_Skulls;
    public Text txt_HighScoreGems;
    public Text txt_HighScoreSkulls;

    // Start is called before the first frame update
    void Start()
    {
        //Check if we came back from game
        checkLevelMusic();

        //Set Sound to "On"
        if(PlayerPrefs.GetString("Sound") == null || PlayerPrefs.GetString("Sound") == "On")
        {
            soundOn();
        }
        else if(PlayerPrefs.GetString("Sound") == "Off")
        {
            soundOff();
        }

        //High Scores
        highScore_Gems = PlayerPrefs.GetInt("HighScoreGems");
        highScore_Skulls = PlayerPrefs.GetInt("HighScoreSkulls");
        //highScore = PlayerPrefs.GetInt("HighScore");
        //txt_HighScore.text = (txt_HighScore.text + highScore_Gems.ToString());
        txt_HighScoreGems.text = "x" + highScore_Gems.ToString();
        txt_HighScoreSkulls.text = "x" + highScore_Skulls.ToString();
    }

    public void Button_NewGame()
    {
        //Load the game scene
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        SceneManager.LoadScene("Level_1");
    }

    public void Button_About()
    {
        //Load the About scene
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        SceneManager.LoadScene("About");
    }

    public void Button_Exit()
    {
        //Exit the game
        exitGame();
    }

    public void Button_X()
    {
        //Exit the game
        exitGame();
    }

    public void Button_Sound()
    {
        //Flip the image and change the global Sound setting that I need to create
        string soundSetting = PlayerPrefs.GetString("Sound");
        if(soundSetting == "On")
        {
            soundOff();
        }
        else if(soundSetting == "Off")
        {
            soundOn();
        }
    }

    private void exitGame()
    {
        //Exit the game
        Application.Quit();
    }

    public void playMainMenuMusic()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<musicMainHandler>().PlayMusic();
    }

    public void turnOffMainMenuMusic()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<musicMainHandler>().StopMusic();
    }
    
    private void soundOn()
    {
        PlayerPrefs.SetString("Sound", "On");

        //flip the icon
        btnSound.image.sprite = sprSoundOn;

        //Set the sound on
        playMainMenuMusic();
    }

    private void soundOff()
    {
        PlayerPrefs.SetString("Sound", "Off");

        //flip the icon
        btnSound.image.sprite = sprSoundOff;

        //Turn sound off
        turnOffMainMenuMusic();
    }

    private void checkLevelMusic()
    {
        try
        {
            if (GameObject.FindGameObjectsWithTag("Level1Music").Length > 0)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Level1Music")[0]);
            }
        }
        catch
        {
            Debug.Log("Did not come back from game");
        }
        
    }
}
