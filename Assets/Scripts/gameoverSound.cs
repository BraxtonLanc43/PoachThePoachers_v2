using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Temp");
    }

    public void playGameOverSound()
    {
        if (PlayerPrefs.GetString("Sound") == "On")
        {
            stopGameMusic();
            Debug.Log("Playing GameOver sound");
            audioSource.Play();
        }
    }

    private void stopGameMusic()
    {
        Debug.Log("Stopping game music");
        GameObject.FindGameObjectWithTag("Level1Music").GetComponent<musicLevel1>().StopMusic();
    }
}
