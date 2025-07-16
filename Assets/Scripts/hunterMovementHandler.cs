using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hunterMovementHandler : MonoBehaviour
{
    //Our hunters 
    public hunterController controller;
    public Animator animator;
    private bool jump = false;
    private GameObject lvlManager;
    private bool shouldPauseDeath;

    //Bullets and shooting
    public Transform firepoint;
    public GameObject prefab_HunterBullet1;
    public GameObject prefab_HunterBullet2;
    public GameObject prefab_HunterDynamite;

    //Health
    public int health = 100;
    public bool isAlive = true;
    public healthBars healthBar;

    //Entering scene
    public bool isMovingIntoScene;
    private GameObject pointToMoveToStart;
    private float runSpeed = 5.0f;
    private bool shouldRun = true;

    //AI handling
    private float shootStartTime;
    private float jumpStartTime;

    //Skull score
    private int scoreSkull = 0;
    private GameObject text_SkullScore;

    // Start is called before the first frame update
    void Start()
    {
        lvlManager = GameObject.FindGameObjectWithTag("LevelManager");
        healthBar.setMaxHealth(100);
        isMovingIntoScene = true;
        pointToMoveToStart = GameObject.FindGameObjectWithTag("PointToSpawnToStartHunter");
        text_SkullScore = GameObject.FindGameObjectWithTag("SkullScoreText");
        shouldPauseDeath = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (lvlManager.GetComponent<levelManager>().gameIsActive)
        {
            //Moving into scene after we've been spawned off-screen
            if (shouldRun)
            {
                if ((transform.position.x) != pointToMoveToStart.transform.position.x)
                {
                    //Run animation
                    animator.SetBool("isRun", true);
                    transform.position = Vector3.MoveTowards(transform.position, pointToMoveToStart.transform.position, runSpeed * Time.deltaTime);
                }
                else
                {
                    shootStartTime = Time.time;
                    jumpStartTime = Time.time;
                    shouldRun = false;
                    animator.SetBool("isRun", false);
                }
            }
            else if(isAlive)
            {
                //Possibly shoot: every 2 seconds, the Hunter wil potentially shoot
                if (Time.time - shootStartTime > 1.0)
                {
                    toShoot();
                    shootStartTime = Time.time;
                }
                //Possibly Jump: Don't jump all the time, but every once in a while, maybe jump
                if (Time.time - jumpStartTime > 1.3)
                {
                    toJump();
                    jumpStartTime = Time.time;
                }
            }
        }        
    }

    public void onHunterLandEvent()
    {
        animator.SetBool("isJumping", false);
    }

    void ShootWeapon(GameObject bulletPrefab)
    {
        animator.SetTrigger("isShoot");

        //Randomize the Y point a little
        float fire_x = firepoint.position.x;
        float fire_z = firepoint.position.z;
        float fire_y = firepoint.position.y;
        
        //Random #
        float randomNum = Random.Range(0, 4);
        if(randomNum == 1)
        {
            fire_y = (fire_y - 1);  //this puts bullet spawn around -4
        }
        else if(randomNum == 2 || randomNum == 3)
        {
            fire_y = (fire_y + (randomNum - 1));  //this puts bullet spawn around (-2 <> -1)
        }

        //Now create the firepoint
        Vector3 firepoint_New = new Vector3(fire_x, fire_y, fire_z);
        Instantiate(bulletPrefab, firepoint_New, firepoint.rotation);
    }

    public void takeDamage(GameObject obj_InflictingDamage)
    {
        animator.SetTrigger("isTakeDamage");

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
        isAlive = false;
        //Debug.Log("DEATH: Hunter");
        animator.SetTrigger("isDeath");
        addToSkullScore();
        triggerDisappear();
    }

    IEnumerator disappear(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    public void triggerDisappear()
    {
        StartCoroutine(disappear(2.0f));
    }

    public void victory()
    {
        animator.SetBool("isCelebrate", true);
    }

    //AI - Jumping and shooting
    private void toJump()
    {
        //re-using the 'shouldRun' variable because we don't want to shoot until we've made it to starting point
        if (!shouldRun)
        {
            //Roll a random number 1-10, if 1-8 then don't jump, if 9-10 then jump
            int shouldJump = Random.Range(1, 11);
            if (shouldJump >= 8)
            {
                controller.Move(0f, true);
                jump = true;

                //jump animation
                animator.SetBool("isJumping", true);
            }
               
        }
    }

    private void toShoot()
    {
        //re-using the 'shouldRun' variable because we don't want to shoot until we've made it to starting point
        if (!shouldRun)
        {
            //Roll a random number 1-10, if 1-8 then shoot, if 9-10 then don't shoot
            int shouldShoot = Random.Range(1, 11);
            if (shouldShoot >= 1 && shouldShoot <= 7)
                ShootWeapon(prefab_HunterBullet1);
        }
    }

    public void deathAnimationOnly()
    {
        animator.SetBool("isCelebrate", false);
        animator.SetTrigger("isDeath");
    }

    public int getSkullScore()
    {
        Text theText = text_SkullScore.GetComponent<Text>();
        int currentScore = int.Parse((theText.text.Substring(1, theText.text.Length - 1)));

        return currentScore;
    }

    public void addToSkullScore()
    {
        int currentScore = getSkullScore();
        currentScore = (currentScore + 1);

        Text theText = text_SkullScore.GetComponent<Text>();
        theText.text = "x" + currentScore.ToString();
    }
}
