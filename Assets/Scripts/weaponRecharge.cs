using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponRecharge : MonoBehaviour
{
    //Fire1
    public int chargeAmt_Fire1 = 100;
    public healthBars chargeBar_Fire1;
    private int rate_Fire1 = 2;

    //Fire2
    public int chargeAmt_Fire2 = 100;
    public healthBars chargeBar_Fire2;
    private int rate_Fire2 = 2;

    //FireBlue
    public int chargeAmt_FireBlue = 100;
    public healthBars chargeBar_FireBlue;
    private int rate_FireBlue = 5;

    //Laser
    public int chargeAmt_Laser = 100;
    public healthBars chargeBar_Laser;
    private int rate_Laser = 10;

    // Start is called before the first frame update
    void Start()
    {
        allBarsMax();
    }

    public void ChargeBar_Weapon_Use(GameObject bulletPrefab)
    {
        if(bulletPrefab.tag == "Fire1")
        {
            chargeAmt_Fire1 = 0;
            chargeBar_Fire1.setHealth(chargeAmt_Fire1);
            StartCoroutine(regenFire1());
        }
        else if (bulletPrefab.tag == "Fire2")
        {
            chargeAmt_Fire2 = 0;
            chargeBar_Fire2.setHealth(chargeAmt_Fire2);
            StartCoroutine(regenFire2());
        }
        else if (bulletPrefab.tag == "FireBlue")
        {
            chargeAmt_FireBlue = 0;
            chargeBar_FireBlue.setHealth(chargeAmt_FireBlue);
            StartCoroutine(regenFireBlue());
        }
        else if (bulletPrefab.tag == "BulletLaser")
        {
            chargeAmt_Laser = 0;
            chargeBar_Laser.setHealth(chargeAmt_Laser);
            StartCoroutine(regenLaser());
        }

        
    }

    IEnumerator regenFire1()
    {
        //If we're already full, do nothing
        if (chargeAmt_Fire1 < 100)
        {
            //Otherwise, regen over 'x' period
            float elapsedTime = 0.0f;
            while (elapsedTime < rate_Fire1)
            {
                //Update the bar here
                elapsedTime += Time.deltaTime;

                //Get percentage of the elapsed time
                float amountCharged = (elapsedTime / rate_Fire1);
                int amountToUpdateChargeBar = Mathf.RoundToInt(amountCharged * 100);
                chargeAmt_Fire1 = amountToUpdateChargeBar;

                if (chargeAmt_Fire1 > 100)
                    chargeAmt_Fire1 = 100;
                chargeBar_Fire1.setHealth(chargeAmt_Fire1);

                yield return 0;
            }
        }

        yield return 0;

    }

    IEnumerator regenFire2()
    {
        //If we're already full, do nothing
        if (chargeAmt_Fire2 < 100)
        {
            //Otherwise, regen over 'x' period
            float elapsedTime = 0.0f;
            while (elapsedTime < rate_Fire2)
            {
                //Update the bar here
                elapsedTime += Time.deltaTime;

                //Get percentage of the elapsed time
                float amountCharged = (elapsedTime / rate_Fire2);
                int amountToUpdateChargeBar = Mathf.RoundToInt(amountCharged * 100);
                chargeAmt_Fire2 = amountToUpdateChargeBar;

                if (chargeAmt_Fire2 > 100)
                    chargeAmt_Fire2 = 100;
                chargeBar_Fire2.setHealth(chargeAmt_Fire2);

                yield return 0;
            }
        }

        yield return 0;

    }

    IEnumerator regenFireBlue()
    {
        //If we're already full, do nothing
        if (chargeAmt_FireBlue < 100)
        {
            //Otherwise, regen over 'x' period
            float elapsedTime = 0.0f;
            while (elapsedTime < rate_FireBlue)
            {
                //Update the bar here
                elapsedTime += Time.deltaTime;

                //Get percentage of the elapsed time
                float amountCharged = (elapsedTime / rate_FireBlue);
                int amountToUpdateChargeBar = Mathf.RoundToInt(amountCharged * 100);
                chargeAmt_FireBlue = amountToUpdateChargeBar;

                if (chargeAmt_FireBlue > 100)
                    chargeAmt_FireBlue = 100;
                chargeBar_FireBlue.setHealth(chargeAmt_FireBlue);

                yield return 0;
            }
        }

        yield return 0;

    }

    IEnumerator regenLaser()
    {
        //If we're already full, do nothing
        if (chargeAmt_Laser < 100)
        {
            //Otherwise, regen over 'x' period
            float elapsedTime = 0.0f;
            while (elapsedTime < rate_Laser)
            {
                //Update the bar here
                elapsedTime += Time.deltaTime;

                //Get percentage of the elapsed time
                float amountCharged = (elapsedTime / rate_Laser);
                int amountToUpdateChargeBar = Mathf.RoundToInt(amountCharged * 100);
                chargeAmt_Laser = amountToUpdateChargeBar;

                if (chargeAmt_Laser > 100)
                    chargeAmt_Laser = 100;
                chargeBar_Laser.setHealth(chargeAmt_Laser);

                yield return 0;
            }
        }

        yield return 0;

    }

    private void allBarsMax()
    {
        chargeBar_Fire1.setHealth(chargeAmt_Fire1);
        chargeBar_Fire2.setHealth(chargeAmt_Fire2);
        chargeBar_FireBlue.setHealth(chargeAmt_FireBlue);
        chargeBar_Laser.setHealth(chargeAmt_Laser);
    }

    public bool isRecharged(GameObject bulletPrefab)
    {
        bool charged = false;

        if (bulletPrefab.tag == "Fire1")
        {
            if (chargeAmt_Fire1 == 100)
                charged = true;
        }
        else if (bulletPrefab.tag == "Fire2")
        {
            if (chargeAmt_Fire2 == 100)
                charged = true;
        }
        else if (bulletPrefab.tag == "FireBlue")
        {
            if (chargeAmt_FireBlue == 100)
                charged = true;
        }
        else if (bulletPrefab.tag == "BulletLaser")
        {
            if (chargeAmt_Laser == 100)
                charged = true;
        }
        
        return charged;
    }
}
