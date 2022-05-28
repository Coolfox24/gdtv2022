using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSO : ItemScriptableObject
{
    [field: SerializeField] public string WeaponType {get; private set;}

    [SerializeField] protected float baseDmg;
    [field: SerializeField] public float cooldown {get; protected set;}
    [SerializeField] protected float speed;
    [SerializeField] protected int hits;
    [SerializeField] protected GameObject weaponProjectile;
    [field: SerializeField] public float timeBetweenExtraShotsPercent {get; protected set;}

    public abstract void FireWeapon(Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos, Weapon wep);

    protected float GetSwingSpeed(float speedCalc)
    {
        return speed - (speed * ((0.00125f * (speedCalc * speedCalc)) - (0.0625f * speedCalc))); //This currently makes it take longer so need to inverse
    }
}
