using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHandler : MonoBehaviour
{
    private float speed = 20.0f;
    public Rigidbody2D rb;
    public int bulletDamage = 0;
    private Direction directionToTravel;
    Vector3 vectorDirectionToTravel;
    private IgnoreCollisionsWith toIgnoreCollision;
    private float xOffset_Destroy = 25f;
    private GameObject player;
    public float cooldown = 0.0f;

    //Audio
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "Fire1" || this.gameObject.tag == "Fire2" || this.gameObject.tag == "FireBlue" || this.gameObject.tag == "BulletLaser")
        {
            audioSource = GetComponent<AudioSource>();
            playSound();
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        setProps();
        if (directionToTravel == Direction.Left)
            vectorDirectionToTravel = transform.right * -1;
        else if (directionToTravel == Direction.Right)
            vectorDirectionToTravel = transform.right;
        rb.velocity = vectorDirectionToTravel * speed;
    }

    // Update is called once per frame
    void Update()
    {
        destroyOffScreen();
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (isAProjectile(col))
        {
            // Ignore the collision with other bullets
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col);
        }
        if (col.transform.tag == "Hunter" && toIgnoreCollision != IgnoreCollisionsWith.Hunter)
        {
            Destroy(gameObject);
            hunterMovementHandler hunter_MvHnd = col.GetComponent<hunterMovementHandler>();
            if (hunter_MvHnd.isAlive)
            {
                hunter_MvHnd.takeDamage(this.gameObject);
            }
        }
        else if (col.transform.tag == "Player" && toIgnoreCollision != IgnoreCollisionsWith.Player)
        {
            Destroy(gameObject);
            playerMovementHandler player_MvHnd = col.GetComponent<playerMovementHandler>();
            if (player_MvHnd.isAlive)
            {
                player_MvHnd.takeDamage(this.gameObject);
            }
        }
    }

    void setProps()
    {
        switch (gameObject.tag)
        {
            case "Fire1":
                bulletDamage = 10;
                speed = 20.0f;
                directionToTravel = Direction.Right;
                toIgnoreCollision = IgnoreCollisionsWith.Player;
                cooldown = 1.35f;
                break;
            case "Fire2":
                bulletDamage = 10;
                speed = 20.0f;
                directionToTravel = Direction.Right;
                toIgnoreCollision = IgnoreCollisionsWith.Player;
                cooldown = 1.35f;
                break;
            case "FireBlue":
                bulletDamage = 25;
                speed = 20.0f;
                directionToTravel = Direction.Right;
                toIgnoreCollision = IgnoreCollisionsWith.Player;
                cooldown = 5.0f;
                break;
            case "BulletLaser":
                bulletDamage = 50;
                speed = 40.0f;
                directionToTravel = Direction.Right;
                toIgnoreCollision = IgnoreCollisionsWith.Player;
                cooldown = 10.0f;
                break;
            case "BulletSlice":
                bulletDamage = 80;
                speed = 20.0f;
                directionToTravel = Direction.Right;
                toIgnoreCollision = IgnoreCollisionsWith.Player;
                break;
            case "HunterBullet1":
                bulletDamage = 20;
                speed = 20.0f;
                directionToTravel = Direction.Left;
                toIgnoreCollision = IgnoreCollisionsWith.Hunter;
                break;
            case "HunterBullet2":
                bulletDamage = 20;
                speed = 20.0f;
                directionToTravel = Direction.Left;
                toIgnoreCollision = IgnoreCollisionsWith.Hunter;
                break;
            case "HunterDynamite":
                bulletDamage = 80;
                speed = 20.0f;
                directionToTravel = Direction.Left;
                toIgnoreCollision = IgnoreCollisionsWith.Hunter;
                break;
            default:
                Debug.Log("Default case?");
                bulletDamage = 0;
                break;
        }
    }

    private bool isAProjectile(Collider2D obj_Projectile)
    {
        if (obj_Projectile.transform.tag == "Fire1" || obj_Projectile.transform.tag == "Fire2" || obj_Projectile.transform.tag == "FireBlue" || obj_Projectile.transform.tag == "BulletLaser" || obj_Projectile.transform.tag == "BulletSlice" || obj_Projectile.transform.tag == "HunterBullet1" || obj_Projectile.transform.tag == "HunterBullet2" || obj_Projectile.transform.tag == "HunterDynamite")
            return true;
        else
            return false;
    }

    private void destroyOffScreen()
    {
        //if it 25f past the player, or 75f away from player, destroy
        if ((gameObject.transform.position.x <= (player.transform.position.x - xOffset_Destroy)) || (gameObject.transform.position.x >= (player.transform.position.x + (xOffset_Destroy*3))))
        {
            Destroy(gameObject);
        }
    }

    private void playSound()
    {
        if(PlayerPrefs.GetString("Sound") == "On")
        {
            audioSource.Play();
        }
    }

    enum Direction
    {
        Left,
        Right
    }

    enum IgnoreCollisionsWith
    {
        Player,
        Hunter
    }
}
