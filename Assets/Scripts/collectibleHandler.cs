using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleHandler : MonoBehaviour
{
    public int pointValue;
    public Rigidbody2D rb;
    private float speed = 16.0f;
    private float xOffset_Destroy = 25f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setProps();
        rb.velocity = (transform.right * -1) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        destroyOffScreen();
    }

    //if it 25f past the player
    private void destroyOffScreen()
    {
        //TO-DO :: Implement how far offscreen it should go until it dies
        if(gameObject.transform.position.x <= (player.transform.position.x - xOffset_Destroy))
        {
            Destroy(gameObject);
        }
    }

    private void setProps()
    {
        switch (gameObject.tag)
        {
            case "GemGreen":
                pointValue = 1;
                break;
            case "GemBlue":
                pointValue = 5;
                break;
            case "GemRed":
                pointValue = 10;
                break;
            default:
                Debug.Log("Default case?");
                break;
        }
    }

    
}
