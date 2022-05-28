using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    //Prefab for loot which we'll give the item of    

    private const int commonWeight = 50;
    private const int uncommonWeight = 30;
    private const int rareWeight = 15;
    private const int legendaryWeight = 5;

    [SerializeField] private LootTableSO defaultLoot;
    [SerializeField] private ItemPickup lootPrefab;

    [SerializeField] private PlayerStateMachine psm;

    public void GenerateLoot(LootTableSO loot, Vector2 position)
    {
        ItemScriptableObject itemCreated = GenerateItem(loot);
        if(itemCreated == null)
        {
            return;
        }

        Item.Rarity rarity = GenerateRarity(loot);
        Item newItem = null;
        if(itemCreated is WeaponSO)
        {
            newItem = new Weapon((WeaponSO)itemCreated, rarity);
        }
        else if(itemCreated is ArmorItemSO)
        {
            newItem = new Armor((ArmorItemSO)itemCreated, rarity);
        }   
        if(newItem == null)
        {
            return;
        }

        GameObject g = Instantiate(lootPrefab.gameObject, position, Quaternion.identity);
        g.GetComponent<ItemPickup>().item = newItem;
    }   

    protected ItemScriptableObject GenerateItem(LootTableSO loot)
    {
        float dropLoot = Random.Range(0f, 1f);
        int lootAdjust = 0;
        if(psm != null)
        {
            lootAdjust += (int) psm.PlayerStats.Luck + (int)psm.Equipment.armorStats.Luck;
        }
        if(loot == null)
        {
            //Generate Default Loot
            if(dropLoot <= defaultLoot.LootDropChance)
            {
                return defaultLoot.GetDroppedItem(lootAdjust);
            }
            return null;
        }
        
        if(loot.SpecificDropChance && dropLoot <= loot.LootDropChance)
        {
            //Generate
            return loot.GetDroppedItem(lootAdjust);
        }

        if(dropLoot <= defaultLoot.LootDropChance)
        {
            //Generate
             return loot.GetDroppedItem(lootAdjust);
        }   

        return null;
    }

    protected Item.Rarity GenerateRarity(LootTableSO loot)
    {
        if(loot == null)
        {
            return GenerateRarity(defaultLoot);
        }

        int rarityWeight = 0;
        foreach(Item.Rarity r in loot.PossibleRarity)
        {
            switch(r)
            {
                case Item.Rarity.common:
                    rarityWeight += commonWeight;
                    break;
                case Item.Rarity.uncommon:
                    rarityWeight += uncommonWeight;
                    break;
                case Item.Rarity.rare:
                    rarityWeight += rareWeight;
                    break;
                case Item.Rarity.legendary:
                    rarityWeight += legendaryWeight;
                    break;
            }
        }

        int randomNum = Random.Range(0, rarityWeight);

        foreach(Item.Rarity r in loot.PossibleRarity)
        {
            switch(r)
            {
                case Item.Rarity.common:
                    if (randomNum < commonWeight)
                    {
                        return Item.Rarity.common;
                    }
                    randomNum -= commonWeight;
                    break;
                case Item.Rarity.uncommon:
                    if (randomNum < uncommonWeight)
                    {
                        return Item.Rarity.uncommon;
                    }
                    randomNum -= uncommonWeight;
                    break;
                case Item.Rarity.rare:
                    if (randomNum < rareWeight)
                    {
                        return Item.Rarity.rare;
                    }
                    randomNum -= rareWeight;
                    break;
                case Item.Rarity.legendary:
                    if (randomNum < legendaryWeight)
                    {
                        return Item.Rarity.legendary;
                    }
                    randomNum -= legendaryWeight;
                    break;
            }
        }
        Debug.Log("ERR generating rarity");
        return Item.Rarity.common;

    }
}
