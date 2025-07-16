using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class backgroundManager : MonoBehaviour
{
    //Class vars
    private float speed = 0.11f;
    GameObject mainCamera;
    public GameObject levelManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.GetComponent<levelManager>().gameIsActive)
        {
            //Scroll the background
            scrollBackground();
        }
    }

    //Method to scroll the background
    private void scrollBackground()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

}