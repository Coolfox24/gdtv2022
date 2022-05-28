using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Weapon", menuName= "Weapons/Gravity", order= 1)]
public class GravityWeapon : WeaponSO
{
    public float BaseProjectileAmount = 1;
    public float SpreadRange = 0.2f;

    public override void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep)
    {
        for(int i = 0 ; i < BaseProjectileAmount + stats.Proj_amount + armorStats.Proj_amount; i++)
        {
            GameObject w = Instantiate(weaponProjectile, playerPos.position, Quaternion.identity);
            w.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-SpreadRange, SpreadRange), (2 + speed + stats.Proj_speed + armorStats.Proj_speed)); 

            Projectile p = w.GetComponent<AxeProjectiles>();

            p?.Setup(baseDmg + stats.Damage + armorStats.Damage,
                 hits + (int)stats.Proj_passthrough + (int)armorStats.Proj_passthrough
                );
        }
    }
}
