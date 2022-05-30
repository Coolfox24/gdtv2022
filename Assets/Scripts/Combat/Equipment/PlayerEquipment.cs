using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [field: SerializeField] public PlayerStats armorStats {get; private set;}

    [field: SerializeField] public List<Weapon> currentWeapons {get; private set;}
    [SerializeField] private WeaponSO startingWeapon;

    public Armor helmet  {get; private set;}
    public Armor body  {get; private set;}
    public Armor legs  {get; private set;}
    public Armor access1  {get; private set;}
    public Armor  access2 {get; private set;}

    void Start()
    {
        UpdatePlayerStats();
        currentWeapons = new List<Weapon>();
        
        for(int i = 0; i < 5; i++)
        {
            currentWeapons.Add(null); //Initialize list at length
        }
        
        //ChangeWeapon(new Weapon(startingWeapon, Item.Rarity.common), 0);

        //Change this to be random at later point
    }

    public void ChangeItem(Armor item, ArmorItemSO.ARMOR_SLOTS slot, int accessSlot)
    {
        switch (slot)
        {
            case ArmorItemSO.ARMOR_SLOTS.HEAD:
                this.helmet = item;
                break;
            case ArmorItemSO.ARMOR_SLOTS.CHEST:
                this.body = item;
                break;
            case ArmorItemSO.ARMOR_SLOTS.LEGS:
                this.legs = item;
                break;
            case ArmorItemSO.ARMOR_SLOTS.ACCESSORY:
                if(accessSlot == 0)
                {
                    access1 = item;
                }
                else
                {
                    access2 = item;
                }
                //Need to ascertain which 1 to replace here
                break;
        }

        UpdatePlayerStats();   
    }

    public void ChangeWeapon(Weapon item, int WeaponIndex)
    {
        if(WeaponIndex >= 5)
        {
            Debug.Log("Tried to assign more than 5 weapons");
            return;
        }

        currentWeapons[WeaponIndex] = item;
    }

    private void UpdatePlayerStats()
    {
        armorStats = new PlayerStats();
        ArmorAddedStats(legs);
        ArmorAddedStats(body);
        ArmorAddedStats(helmet);
        ArmorAddedStats(access1);
        ArmorAddedStats(access2);
    }

    private void ArmorAddedStats(Armor item)
    {
        if(item == null)
        {
            return;
        }

        foreach(Item.ItemStats stats in item.stats)
        {
            //TODO finish this implementation
            switch(stats.stat)
            {
                case PlayerStats.PLAYER_STATS.SPEED:
                    armorStats.Speed += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.COOLDOWN:
                    armorStats.Cooldown += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.AREA:
                    armorStats.Area += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.DAMAGE:
                    armorStats.Damage += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.DAMAGE_REDUCTION:
                    armorStats.Damage_Reduction += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.EXTRA_SWINGS:
                    armorStats.Extra_Swings += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.LUCK:
                    armorStats.Luck += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.PROJECTILE_PASSTHROUGH:
                    armorStats.Proj_passthrough += stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.PROJECTILE_AMOUNT:
                    armorStats.Proj_amount += (int)stats.amount;
                    break;
                case PlayerStats.PLAYER_STATS.INVINCIBILITY_TIME:
                    armorStats.IFrames += stats.amount;
                    break;
            }
        }
    }
}
