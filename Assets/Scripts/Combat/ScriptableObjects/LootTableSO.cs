using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LootTable", menuName ="Loot Table", order =1)]
public class LootTableSO : ScriptableObject
{   

    public bool SpecificRarity;
    public List<Item.Rarity> PossibleRarity;
    public bool SpecificDropChance;
    public float LootDropChance;

    public bool SpecificList;
    [SerializeField] private List<ItemWeightings> ListOfItems;

    //Called when specificList == true
    public ItemScriptableObject GetDroppedItem()
    {
        int totalWeight = 0;
        foreach(ItemWeightings iWeight in ListOfItems)
        {
            totalWeight += iWeight.weighting;
        }
        int randomNum = Random.Range(0, totalWeight);

        foreach(ItemWeightings item in ListOfItems)
        {
            if(randomNum <= item.weighting)
            {
                int randomWithin = Random.Range(0, item.items.Count);
                return item.items[randomWithin];
            }
            else
            {
                totalWeight -= item.weighting;
            }
        }

        return null;
    }

    public ItemScriptableObject GetDroppedItem(int weightAdjust)
    {
        int totalWeight = 0;
        foreach(ItemWeightings iWeight in ListOfItems)
        {
            if(iWeight.weighting < 30)
            {
                totalWeight += weightAdjust;
            }
            else
            {
                totalWeight -= weightAdjust;
            }
            totalWeight += iWeight.weighting;
        }
        int randomNum = Random.Range(0, totalWeight);

        foreach(ItemWeightings item in ListOfItems)
        {
            if(randomNum <= (item.weighting + (item.weighting < 30 ? weightAdjust : -weightAdjust)))
            {
                int randomWithin = Random.Range(0, item.items.Count);
                return item.items[randomWithin];
            }
            else
            {
                totalWeight -= item.weighting;
                if(item.weighting < 30)
                {
                    totalWeight -= weightAdjust;
                }
                else
                {
                    totalWeight += weightAdjust;
                }
            }
        }

        return null;
    }
    
    [System.Serializable]
    private struct ItemWeightings{
        public List<ItemScriptableObject> items;
        public int weighting;
    }
}
