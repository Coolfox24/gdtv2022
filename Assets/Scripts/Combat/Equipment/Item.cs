using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
    public Sprite icon;
    public string itemName; 

    [SerializeField] public Rarity rarity {get; protected set;}
    [field: SerializeField] public List<ItemStats> stats {get; protected set;}

    public Item(Rarity rarity, Sprite icon)
    {
        this.rarity = rarity;
        this.icon = icon;
    }

    protected void GenerateRandomStats(Rarity rarity)
    {
        int stats = (int)rarity;
        while(stats > 0)
        {
            stats -= GenerateStat((int)rarity);
        }
    }

    private int GenerateStat(int rarity)
    {
        PlayerStats.PLAYER_STATS r =  (PlayerStats.PLAYER_STATS)Random.Range(0, System.Enum.GetNames(typeof(PlayerStats.PLAYER_STATS)).Length);
        int amount = Random.Range(1, rarity);
        bool exists = false;
        for(int i = 0; i < stats.Count; i++)
        {
            if(stats[i].stat == r)
            {
                ItemStats t = stats[i];
                t.amount += amount;
                exists = true;
                stats[i] = t;
            }
        }
        if(!exists)
        {
            ItemStats stat;
            stat.stat = r;
            stat.amount = amount;
            stats.Add(stat);
        }

        return amount;
    }


    [System.Serializable]
    public struct ItemStats{
        public PlayerStats.PLAYER_STATS stat;
        public float amount;        
    }

    public enum Rarity
    {
        common = 3,
        uncommon = 5,
        rare = 7,
        legendary = 10
    }
}
