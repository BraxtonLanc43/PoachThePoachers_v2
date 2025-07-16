using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHandler : MonoBehaviour
{
    //LevelManager fields
    public GameObject levelManager;
    private GameObject player;
    public GameObject point_HunterDestination;

    //Public Prefabs
    public GameObject prefab_GuyHunter;
    public GameObject prefab_GalHunter;
    public GameObject prefab_GreenGem;
    public GameObject prefab_BlueGem;
    public GameObject prefab_RedGem;

    //Spawned Prefabs
    List<GameObject> spawned_Hunters = new List<GameObject>();
    List<GameObject> spawned_Gems = new List<GameObject>();

    //Counts of objects on the field
    int count_allHunters = 0;
    int count_GreenGems = 0;
    int count_BlueGems = 0;
    int count_RedGems = 0;
    int count_AllGems = 0;

    //Spawn values
    private float spawnOffset_x = 30f;
    private float gemStartTime;
    private float hunterStartTime;

    //Other
    private bool isPaused;
    List<GameObject> activeGems;

    void Awake()
    {
        gemStartTime = Time.time;
        hunterStartTime = Time.time;
        isPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        //Only do things while the game is active
        if (levelManager.GetComponent<levelManager>().gameIsActive)
        {
            //Debug.Log("It's the Gem Handler");
            //Get Spawns: every 1.3 seconds, we'll potentially spawn gems
            if (Time.time - gemStartTime > 1.3)
            {
                spawnGemHandler();
                gemStartTime = Time.time;
            }

            //Hunter Spawns: every second, we'll potentially spawn a Hunter
            //Debug.Log("HunterStartTime: " + hunterStartTime);
            //Debug.Log("Time.time-hunterStartTime: " + (Time.time - hunterStartTime));
            if (Time.time - hunterStartTime > 1)
            {
                spawnHunterHandler();
                hunterStartTime = Time.time;
            }
        }
        
    }

    private List<GameObject> getAllGems()
    {
        GameObject[] greenGems = GameObject.FindGameObjectsWithTag("GemGreen");
        GameObject[] blueGems = GameObject.FindGameObjectsWithTag("GemBlue");
        GameObject[] redGems = GameObject.FindGameObjectsWithTag("GemRed");
        List<GameObject> allGems = new List<GameObject>();

        if(greenGems.Length > 0)
        {
            for(int i = 0; i < greenGems.Length; i++)
            {
                allGems.Add(greenGems[i]);
            }
        }
        if (blueGems.Length > 0)
        {
            for (int i = 0; i < blueGems.Length; i++)
            {
                allGems.Add(blueGems[i]);
            }
        }
        if (redGems.Length > 0)
        {
            for (int i = 0; i < redGems.Length; i++)
            {
                allGems.Add(redGems[i]);
            }
        }

        return allGems;
    }

    private int getCount_AllHunters()
    {
        count_allHunters = GameObject.FindGameObjectsWithTag("Hunter").Length;

        return count_allHunters;
    }
    
    private int getCount_AllGems()
    {
        count_GreenGems = GameObject.FindGameObjectsWithTag("GemGreen").Length;
        count_BlueGems = GameObject.FindGameObjectsWithTag("GemBlue").Length;
        count_RedGems = GameObject.FindGameObjectsWithTag("GemRed").Length;
        count_AllGems = count_GreenGems + count_BlueGems + count_RedGems;

        return count_AllGems;
    }

    private void spawnGemHandler()
    {
        //Roll a random number, 1-10
        //if 1-5, GreenGem
        //if 6-8, BlueGem
        //if 9-10, RedGem
        //Debug.Log("Trying to spawn a GEM here");
        int whichGem = Random.Range(1, 10);
        GameObject gemToSpawn = prefab_GreenGem; //placeholder..
        if (whichGem >= 1 && whichGem <= 5)
            gemToSpawn = prefab_GreenGem;
        else if (whichGem >= 6 && whichGem <= 8)
            gemToSpawn = prefab_BlueGem;
        else if (whichGem == 9 || whichGem == 10)
            gemToSpawn = prefab_RedGem;

        //Where to spawn it? 
        //y cooredinate should be aligned with player at lowest, and attainable via jump at highest
        float yTop = getYTopOfScreen();
        float yMin = player.transform.position.y;
        float yMax = yTop * 0.25f;
        float ySpawn = Random.Range(yMin, yMax);

        //x coordinate should be offscreen and come into the scene
        float xSpawn = spawnOffset_x; //Will this hardcoded offset work for other devices?

        //Spawn Gem
        Vector3 spawnPoint = new Vector3(xSpawn, ySpawn, player.transform.position.z);
        GameObject freshSpawn = Instantiate(gemToSpawn);
        freshSpawn.transform.position = spawnPoint;
        //Debug.Log("Spawned Gem: " + gemToSpawn.transform.tag);
    }

    private void spawnHunterHandler()
    {
        //Spawn system
        if (getCount_AllHunters() == 0)
        {
            //spawn enemy off screen
            //Where to spawn it? 
            //y cooredinate should be aligned with player
            float ySpawn = player.transform.position.y;

            //x coordinate should be offscreen and come into the scene
            float xSpawn = spawnOffset_x; //Will this hardcoded offset work for other devices?

            //spawn enemy
            //Gal or Guy hunter?
            GameObject hunterToSpawn = prefab_GalHunter; //placeholder
            int whichHunter = Random.Range(1, 3);
            if (whichHunter == 1)
                hunterToSpawn = prefab_GalHunter;
            else if (whichHunter == 2)
                hunterToSpawn = prefab_GuyHunter;
            //Debug.Log("Got this far..");

            //Spawn Hunter
            Vector3 spawnPoint = new Vector3(xSpawn, ySpawn, player.transform.position.z);
            GameObject freshSpawn = Instantiate(hunterToSpawn);
            freshSpawn.transform.position = spawnPoint;
            //Debug.Log("Spawned Hunter: " + hunterToSpawn.transform.tag);

            

            //enemy stops at a certain point
            freshSpawn.GetComponent<hunterMovementHandler>().animator.SetBool("isRun", false);
            //Debug.Log("Spawned Hunter has reached their destination");

        }
        else
            return;
        
    }

    /*
   * @param none
   * @returns float y
   * @desc gets the y position for the bottom of the screen, based on the screen size
   */
    private float getYTopOfScreen()
    {
        Vector3 worldTop = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -20.5f));

        return worldTop.y;
    }



    /*
    * @param none
    * @returns float y
    * @desc gets the y position for the top of the screen, based on the screen size
    */
    private float getYBottomOfScreen()
    {
        Vector3 worldBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -20.5f));

        return worldBottom.y;
    }


    /*
   * @param none
   * @returns float y
   * @desc gets the y position for the top of the screen, based on the screen size
   */
    private float getXRightOfScreen()
    {
        Vector3 worldRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        return worldRight.x;
    }

    public void pauseSpawns()
    {
        isPaused = true;
    }

    public void unpauseSpawns()
    {
        isPaused = true;
    }
}
