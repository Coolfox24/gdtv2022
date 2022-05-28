using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/ForwardShooting", order= 1)]
public class ForwardShotWeapon : WeaponSO
{
    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {   
        float speed = (this.speed + stats.Proj_speed + armorStats.Proj_speed);
        //Shoot projectile forward
        GameObject proj = Instantiate(weaponProjectile, playerPos.position, Quaternion.identity); //TODO this quaternion needs to be set to facing
        proj.GetComponent<Rigidbody2D>().velocity = facing * (speed + stats.Proj_speed + armorStats.Proj_speed); 
        Projectile p = proj.GetComponent<Projectile>();
        p?.Setup(baseDmg + stats.Damage + armorStats.Damage,
                 hits + (int)stats.Proj_passthrough + (int)armorStats.Proj_passthrough
                );
        p.speed = speed; //VERY NAUGHTY PRACTICES SMH

        proj.transform.localScale *= 1 + ((stats.Area + armorStats.Area) * .2f);
        wep.shotsRemaining --;
        if(wep.shotsRemaining < 0)
        {
            wep.shotsRemaining = stats.Proj_amount + armorStats.Proj_amount;
        }
    }
}
