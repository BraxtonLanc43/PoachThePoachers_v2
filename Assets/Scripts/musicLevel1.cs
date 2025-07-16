using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicLevel1 : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        Debug.Log("Current Sound Setting " + PlayerPrefs.GetString("Sound"));
        //Debug.Log("Found this many Level1Music: " + GameObject.FindGameObjectsWithTag("Level1Music").Length);
        if (GameObject.FindGameObjectsWithTag("Level1Music").Length > 1)
        {
            Debug.Log("Destroying a Level1Music Object");
            Destroy(GameObject.FindGameObjectsWithTag("Level1Music")[0].gameObject);
        }

        if (PlayerPrefs.GetString("Sound") == "On")
        {
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
            PlayMusic();
        }

    }

    public void PlayMusic()
    {
        if (PlayerPrefs.GetString("Sound") == "Off" || _audioSource.isPlaying)
            return;
        else
            _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
