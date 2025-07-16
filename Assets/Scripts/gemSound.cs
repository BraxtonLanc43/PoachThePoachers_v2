using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemSound : MonoBehaviour
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
        
    }

    public void playGemSound()
    {
        if(PlayerPrefs.GetString("Sound") == "On")
        {
            //Debug.Log("Playing gem sound");
            audioSource.Play();
        }
    }
}
