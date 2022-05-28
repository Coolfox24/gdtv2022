using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    WeaponSO weapon; //Test
    
    public int swingsRemaining;
    public int shotsRemaining;
    [SerializeField]private float cooldownRemaining;
    public string WeaponCategory;

    public Weapon(WeaponSO weapon, Item.Rarity rarity) : base(rarity, weapon.icon)
    {
        this.itemName = weapon.itemName;
        this.weapon = weapon;
        this.WeaponCategory = this.weapon.WeaponType;
        this.stats = new List<Item.ItemStats>();
        
        //Generate Random Stats here
        GenerateRandomStats(rarity);
    }

    public void CheckWeaponCooldown(float deltaTime, Vector2 facing, PlayerStats stats, PlayerStats armorStats, Transform playerPos)
    {   
        if(swingsRemaining > 0)
        {
            //Handled in SwingWeapon
            return;
        }
        cooldownRemaining -= deltaTime;
        if(shotsRemaining > 0 && cooldownRemaining < 0)
        {
            weapon.FireWeapon(facing, stats, armorStats, playerPos, this);
            SetCooldown(stats, armorStats, weapon.timeBetweenExtraShotsPercent);
            return;
        }
        if(cooldownRemaining < 0)
        {
            weapon?.FireWeapon(facing, stats, armorStats, playerPos, this);
            SetCooldown(stats, armorStats, weapon.timeBetweenExtraShotsPercent);
        }
    }

    private void SetCooldown(PlayerStats stats, PlayerStats armorStats, float pct)
    {
        if(shotsRemaining > 0)
        {
            cooldownRemaining = (weapon.cooldown - ((weapon.cooldown /2) * ((stats.Cooldown + armorStats.Cooldown)/20))) * pct;
        }
        else
        {
            cooldownRemaining = weapon.cooldown - ((weapon.cooldown /2) * ((stats.Cooldown + armorStats.Cooldown)/20));
        }
    }
}
