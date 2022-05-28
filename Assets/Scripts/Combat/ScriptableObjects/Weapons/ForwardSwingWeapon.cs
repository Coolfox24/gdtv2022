using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/ForwardSwing", order= 1)]
public class ForwardSwingWeapon : WeaponSO
{
    public float SwingRadius = 90;
    public bool SwingSameDirection = false;

    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {

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


        GameObject proj = Instantiate(weaponProjectile, playerPos.position, initialRotation);
        proj.transform.parent = playerPos;
        SwingWeapon sWep = proj.GetComponent<SwingWeapon>();
        float speedCalc = stats.Swing_Speed + armorStats.Swing_Speed;
        sWep.Setup((int)(hits + stats.Extra_Swings + armorStats.Extra_Swings),
                    baseDmg + stats.Damage + armorStats.Damage,
                    this.GetSwingSpeed(speedCalc), //This currently makes it take longer so need to inverse
                    wep,
                    SwingRadius,
                    SwingSameDirection
                );

        proj.transform.localScale *= 1 + ((stats.Area + armorStats.Area) * .2f);
    }
}
