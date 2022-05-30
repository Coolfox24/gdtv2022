using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Armor : Item
{
    private ArmorItemSO armorSO; //Reference of base stats
    public ArmorItemSO.ARMOR_SLOTS slot;

    public Armor(ArmorItemSO armor, Item.Rarity rarity) : base(rarity, armor.icon)
    {
        this.itemName = armor.itemName;
        this.armorSO = armor;
        
        this.stats = new List<Item.ItemStats>(armor.baseStats);

        this.slot = armor.slot;
        //Generate Random Stats here
        GenerateRandomStats(rarity);
    }
}
