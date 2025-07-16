using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsHandler : MonoBehaviour
{
    public playerMovementHandler scrPlayer;
    

    public void button_Jump()
    {
        scrPlayer.jumpFunc();
    }

    public void button_ShootFire()
    {
        scrPlayer.ShootWeapon(scrPlayer.prefab_BulletFire1);
    }

    public void button_ShootBlue()
    {
        scrPlayer.ShootWeapon(scrPlayer.prefab_BulletFireBlue);
    }
    
    public void button_ShootLaser()
    {
        scrPlayer.ShootWeapon(scrPlayer.prefab_BulletLaser);
    }
}
