using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class aboutButtonsHandler : MonoBehaviour
{
    public Text txt_SpeechBubble;
    private string txt_WhyThisGame = "Rhinos are constantly in danger of being poached for their horns. We need to help fight rhino poaching before it is too late. Together we can save rhinos from extinction.";
    private string txt_HowCanIHelp = "Consider donating to one of the many organizations that are actively working to combat poaching!";
    //private string txt_Credits = "Fonts and UI: Rafmanix, " + Environment.NewLine +
    //    "Rhino and Poacher Characters: " + Environment.NewLine +
    //    "Backgrounds: " + Environment.NewLine +
    //    "Sound Effects: ";

    private void Start()
    {
        string soundSetting = PlayerPrefs.GetString("Sound");
    }

    public void btn_WhyThisGame()
    {
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        txt_SpeechBubble.text = txt_WhyThisGame;
    }

    public void btn_HowCanIHelp()
    {
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        txt_SpeechBubble.text = txt_HowCanIHelp;
    }

    public void btn_BackToMainMenu()
    {
        GameObject.FindGameObjectWithTag("PopSound").GetComponent<musicPopSoundHandler>().playPopSound();
        SceneManager.LoadScene("MainMenu");
    }
}
