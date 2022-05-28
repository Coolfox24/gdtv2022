using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/SideThrow", order= 1)]
public class SideThrowWeapon : WeaponSO
{
    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {
        float angle = Mathf.Atan2(facing.x, -facing.y) * Mathf.Rad2Deg;
        Quaternion firstProjRotation = Quaternion.Euler (new Vector3(0f,0f,angle - 90));
        Quaternion secondProjRotation = Quaternion.Euler (new Vector3(0f,0f,angle + 90));

        float speed = (this.speed + stats.Proj_speed + armorStats.Proj_speed);
        //Shoot projectile forward
        GameObject proj1 = Instantiate(weaponProjectile, playerPos.position, firstProjRotation); //TODO this quaternion needs to be set to facing
        proj1.GetComponent<Rigidbody2D>().velocity = proj1.transform.up * speed; 
        Projectile p1 = proj1.GetComponent<Projectile>();
        p1?.Setup(baseDmg + stats.Damage + armorStats.Damage,
                 hits + (int)stats.Proj_passthrough + (int)armorStats.Proj_passthrough
                );
        p1.speed = speed; //VERY NAUGHTY PRACTICES SMH

        proj1.transform.localScale *= 1 + ((stats.Area + armorStats.Area) * .2f);
        
        GameObject proj2 = Instantiate(weaponProjectile, playerPos.position, secondProjRotation); //TODO this quaternion needs to be set to facing
        proj2.GetComponent<Rigidbody2D>().velocity = proj2.transform.up * speed; 
        Projectile p2 = proj2.GetComponent<Projectile>();
        p2?.Setup(baseDmg + stats.Damage + armorStats.Damage,
                 hits + (int)stats.Proj_passthrough + (int)armorStats.Proj_passthrough
                );
        p2.speed = speed; //VERY NAUGHTY PRACTICES SMH

        proj2.transform.localScale *= 1 + ((stats.Area + armorStats.Area) * .2f);

        
        wep.shotsRemaining --;
        if(wep.shotsRemaining < 0)
        {
            wep.shotsRemaining = stats.Proj_amount + armorStats.Proj_amount;
        }
    }

}
