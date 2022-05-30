using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/MultiShot", order= 1)]
public class MultiShotWeapon : WeaponSO
{
    public float FiringArc = 45f;
    public float BaseProjectileAmount = 3;

    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {

        float totalProj = BaseProjectileAmount + stats.Proj_amount + armorStats.Proj_amount;
        float angle = Mathf.Atan2(-facing.x, facing.y) * Mathf.Rad2Deg;
        float arc = FiringArc + (FiringArc * ((stats.Area + armorStats.Area) / 10)); //Every 10 area increases arc of fire by firing arc

        Quaternion left = Quaternion.Euler (new Vector3(0f,0f,angle - (arc/2)));
        Quaternion right = Quaternion.Euler (new Vector3(0f,0f,angle + (arc/2)));

        for(int i = 0; i < totalProj; i++)
        {
            GameObject proj = Instantiate(this.weaponProjectile, playerPos.position, Quaternion.Lerp(left, right, i/(totalProj-1)));
            proj.transform.localScale *= 1 + ((stats.Area + armorStats.Area) * .2f);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.velocity =  proj.transform.up * (speed + stats.Speed + armorStats.Speed); 
            Projectile p = proj.GetComponent<Projectile>();
            p?.Setup(baseDmg + stats.Damage + armorStats.Damage,
                    hits + (int)stats.Proj_passthrough + (int)armorStats.Proj_passthrough
                    );
            
            if(p is HomingProjectile)
            {
                p.speed = speed;
            }
        }
    }
}
