using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ArmorItem", menuName ="Armor", order =1)]
public class ArmorItemSO : ItemScriptableObject
{
    [SerializeField] public ARMOR_SLOTS slot;

    public List<Item.ItemStats> baseStats;
    
    
    public enum ARMOR_SLOTS{
        HEAD,
        CHEST,
        LEGS,
        ACCESSORY
    }
}
