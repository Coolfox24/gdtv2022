using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/SideStab", order= 1)]
public class SideStabbingWeapon : WeaponSO
{
    public float StabLength;

    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {
        //Spawn at 90degree angles to player
      Quaternion initialRotation = Quaternion.identity;

        if(facing.x == 0 && facing.y > 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(facing.x == 0 && facing.y < 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 180);
        }
        else if(facing.y == 0 && facing.x > 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 270);
        }
        else if(facing.y == 0 && facing.x < 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 90);
        }
        else if(facing.y > 0 && facing.x > 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 315);
        }
        else if(facing.y < 0 && facing.x > 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 225);
        }
        else if(facing.y > 0 && facing.x < 0)
        {   
            initialRotation = Quaternion.Euler(0, 0, 45);
        }
        else if(facing.y < 0 && facing.x < 0)
        {
            initialRotation = Quaternion.Euler(0, 0, 135);    
        }


        GameObject leftProj = Instantiate(weaponProjectile, playerPos.position, Quaternion.Euler(0, 0, 90) * initialRotation);
        leftProj.transform.parent = playerPos;
        StabWeapon leftWep = leftProj.GetComponent<StabWeapon>();
        float speedCalc = stats.Swing_Speed + armorStats.Swing_Speed;
        leftWep.Setup((int)(hits + stats.Extra_Swings + armorStats.Extra_Swings),
                    baseDmg + stats.Damage + armorStats.Damage,
                    GetSwingSpeed(speedCalc),
                    wep,
                    StabLength
                );
        
        GameObject rightProj = Instantiate(weaponProjectile, playerPos.position, Quaternion.Euler(0, 0, -90) * initialRotation);
        rightProj.transform.parent = playerPos;
        StabWeapon rightWep = rightProj.GetComponent<StabWeapon>();
        rightWep.Setup((int)(hits + stats.Extra_Swings + armorStats.Extra_Swings),
                    baseDmg + stats.Damage + armorStats.Damage,
                    GetSwingSpeed(speedCalc), //This currently makes it take longer so need to inverse
                    wep,
                    StabLength
                );
    }

}
