using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovementHandler : MonoBehaviour
{
    //Our character 
    public characterController controller;
    public Animator animator;
    private bool jump = false;

    //Score
    public int gameScore = 0;
    public Text textScore;

    //Bullets and shooting
    public Transform firepoint;
    public GameObject prefab_BulletFire1;
    public GameObject prefab_BulletFireBlue;
    public GameObject prefab_BulletFire2;
    public GameObject prefab_BulletLaser;
    public GameObject prefab_BulletSlice;

    //Health
    public int health = 100;
    public bool isAlive = true;
    public healthBars healthBar;

    //Level Manager
    public GameObject levelManager;

    //Weapon cooldown
    public GameObject weaponRechargeHandler;

    //Take damage sound
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        healthBar.setMaxHealth(100);
    }

    private void Update()
    {
        //Only do things while the game is active
        if (levelManager.GetComponent<levelManager>().gameIsActive)
        {
            if (Input.GetKeyDown(KeyCode.H))
                levelManager.GetComponent<levelManager>().setGameInactive();

            if (Input.GetKeyDown(KeyCode.Space) && !jump)
            {
                Debug.Log("Space bar");
                jumpFunc();
            }
            if (Input.GetKeyDown(KeyCode.N) && !jump)
            {
                ShootWeapon(prefab_BulletFire1);
            }
        }
    }

    public void onLandEvent()
    {
        if (isAlive)
        {
            //Debug.Log("onLand - Alive");
            animator.SetBool("isJumping", false);
        }
        else if(!isAlive)   //if we died in mid-air, death once we get down
        {
           // Debug.Log("onLand - Not Alive");
            animator.SetBool("isJumping", false);
            animator.SetTrigger("isDeath");
        }
    }

    public void ShootWeapon(GameObject bulletPrefab)
    {
        //Check if ded
        if (!isAlive)
            return;

        //Check weapon cooldown
        bool chargedToShoot = weaponRechargeHandler.GetComponent<weaponRecharge>().isRecharged(bulletPrefab);
        
        if (!chargedToShoot)
            return;
        
        animator.SetTrigger("isShoot");
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        weaponRechargeHandler.GetComponent<weaponRecharge>().ChargeBar_Weapon_Use(bulletPrefab);
    }

    public void takeDamage(GameObject obj_InflictingDamage)
    {
        //Debug.Log("DAMAGE IS TRUE");
        animator.SetTrigger("isTakeDamage");
        if(PlayerPrefs.GetString("Sound") == "On")
            audioSource.Play();

        if ((health - obj_InflictingDamage.GetComponent<bulletHandler>().bulletDamage) < 0 || (health - obj_InflictingDamage.GetComponent<bulletHandler>().bulletDamage) == 0)
        {
            health = 0;
            healthBar.setHealth(health);
            death();
        }
        else
        {
            health = (health - obj_InflictingDamage.GetComponent<bulletHandler>().bulletDamage);
            healthBar.setHealth(health);
        }
        
    }

    public void death()
    {
        //Debug.Log("DEATH: Player");
        isAlive = false;
        animator.SetTrigger("isDeath");

        GameObject[] hunters = GameObject.FindGameObjectsWithTag("Hunter");
        if (hunters.Length > 0)
        {
            for (int i = 0; i < hunters.Length; i++)
            {
                hunters[i].GetComponent<hunterMovementHandler>().victory();
            }
        }

        levelManager.GetComponent<levelManager>().setGameInactive();
        levelManager.GetComponent<levelManager>().highScoreHandler(getScore());

        //Game over sound
        GameObject.FindGameObjectWithTag("GameOverSound").GetComponent<gameoverSound>().playGameOverSound();
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "GemGreen" || col.transform.tag == "GemBlue" || col.transform.transform.tag == "GemRed")
        {
            //Debug.Log("PLAYER_HANDLER: OnCollisionEnter2D with " + col.transform.tag);
            GameObject.FindGameObjectWithTag("GemSound").GetComponent<gemSound>().playGemSound();
            Destroy(col.gameObject);
            gameScore += col.GetComponent<collectibleHandler>().pointValue;
            setScore(gameScore);
        }
    }

    public void setScore(int newScore)
    {
        string newText = "x" + newScore.ToString();
        textScore.text = newText;
    }

    public string getScore()
    {
        return textScore.text;
    }

    public void jumpFunc()
    {
        //Check if ded
        if (!isAlive)
            return;
        controller.Move(0f, true);
        animator.SetBool("isJumping", true);
    }

    public void celebrationAnimation()
    {
        animator.SetBool("isJetpack", true);
        controller.Move(7.5f, false);
    }
}
