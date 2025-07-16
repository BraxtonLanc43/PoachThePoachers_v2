using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicMainHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        Debug.Log("Found this many MenuMusics: " + GameObject.FindGameObjectsWithTag("MenuMusic").Length);
        if (GameObject.FindGameObjectsWithTag("MenuMusic").Length > 1)
        {
            Debug.Log("Destroying a MainMenu Music Object");
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
        
    }
    
    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
