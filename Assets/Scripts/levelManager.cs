using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public bool gameIsActive;
    private int highScore;

    //GameOverUI managing
    public GameObject gameOverUi;

    //HighScore managing
    public GameObject highScoreCelebrationUi;
    public Text txt_HighScoreScore;
    public GameObject highScoreCelebrationSounds;
    public GameObject gameOverTextUI;
    public Text txt_SkullHighScoreScore;
    public Text txt_SkullScore;

    // Start is called before the first frame update
    void Start()
    {
        gameOverTextUI.SetActive(false);
        highScoreCelebrationUi.SetActive(false);
        gameOverUi.SetActive(false);
        checkStopMenuMusic();
        gameIsActive = true;
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGameInactive()
    {
        gameIsActive = false;
        toggleGameOverUI();
    }

    public void highScoreHandler(string currentScoreText)
    {
        currentScoreText = (currentScoreText.Substring(1, currentScoreText.Length-1));
        int currentScore = int.Parse(currentScoreText);

        //Now include the skull score
        string currentSkullScore = getSkullScoreText();
        int currentSkullScoreNum = int.Parse((currentSkullScore.Substring(1, currentSkullScore.Length - 1)));

        //Combine the two
        int score_Total = 0;
        if(currentSkullScoreNum == 0)
        {
            score_Total = currentScore * 1;
        }
        else
        {
            score_Total = currentScore * currentSkullScoreNum;
        }
        

        if (score_Total > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score_Total);
            PlayerPrefs.SetInt("HighScoreGems", currentScore);
            PlayerPrefs.SetInt("HighScoreSkulls", currentSkullScoreNum);

            //Show little high score celebration
            highScoreCelebrationStartCoroutine();
        }
        else
        {
            cueGameOverText();
        }
    }

    private void checkStopMenuMusic()
    {
        try
        {
            GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<musicMainHandler>().StopMusic();
        } catch
        {
            Debug.Log("Caught stopping menu music");
        }
    }
    
    private void toggleGameOverUI()
    {
        Debug.Log("Setting GameOverUI to Active");
        gameOverUi.SetActive(true);
    }

    IEnumerator highScoreCelebrationCoroutine()
    {
        //Wait 3 seconds
        yield return new WaitForSeconds(3);

        //now do the celebratory things
        highScoreCelebration();
    }

    private void highScoreCelebrationStartCoroutine()
    {
        StartCoroutine(highScoreCelebrationCoroutine());
    }

    private void highScoreCelebration()
    {
        //Make player celebrate
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovementHandler>().celebrationAnimation();

        //Make hunter die if there is one
        try
        {
            GameObject.FindGameObjectWithTag("Hunter").GetComponent<hunterMovementHandler>().deathAnimationOnly();
        }
        catch
        {
            Debug.Log("No hunters found");
        }

        //Enable High Score text thing
        string txtUpdateHighScoreCelebration = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovementHandler>().getScore();
        Debug.Log("CurrentScoreText: " + txtUpdateHighScoreCelebration);
        txt_HighScoreScore.text = txtUpdateHighScoreCelebration;

        //And skull score
        txt_SkullHighScoreScore.text = getSkullScoreText();
        
        highScoreCelebrationUi.SetActive(true);

        //Play a celebratory sound
        if(PlayerPrefs.GetString("Sound") == "On")
            highScoreCelebrationSounds.GetComponent<AudioSource>().Play();
    }

    private void cueGameOverText()
    {
        Debug.Log("CueGameOverText");
        gameOverTextUI.SetActive(true);
    }

    public string getSkullScoreText()
    {
        int currentScore = int.Parse((txt_SkullScore.text.Substring(1, txt_SkullScore.text.Length - 1)));

        string toReturn = "x" + currentScore;

        return toReturn;
    }
}
