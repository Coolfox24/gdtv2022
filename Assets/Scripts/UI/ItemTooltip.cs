using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public GameObject StatList;
    public TooltipStat StatPrefab;

    private List<TooltipStat> curStats;
    private List<TooltipStat> disabledStats;

    public int MaxStats = 10;

    bool isInitialized = false;

    public Color commonColor;
    public Color uncommonColor;
    public Color rareColor;
    public Color legendaryColor;


    public void Setup(Item item)
    {
        if(!isInitialized)
        {
            isInitialized = true;
            curStats = new List<TooltipStat>();
            disabledStats = new List<TooltipStat>();
            Debug.Log("Creating Lists for Tooltips");
            for(int i = 0; i < MaxStats; i++)
            {
                GameObject ttStat = Instantiate(StatPrefab.gameObject);
                ttStat.transform.SetParent(StatList.transform);
                disabledStats.Add(ttStat.GetComponent<TooltipStat>());
                ttStat.SetActive(false);
            }  
        }

        NameText.text = item.itemName;
        switch(item.rarity)
        {
            case Item.Rarity.common:
                NameText.color = commonColor;
                break;
            case Item.Rarity.uncommon:
                NameText.color = uncommonColor;
                break;
            case Item.Rarity.rare:
                NameText.color = rareColor;
                break;
            case Item.Rarity.legendary:
                NameText.color = legendaryColor;
                break;

        }



        curStats.RemoveAll(i => 
                            {
                                    i.gameObject.SetActive(false); 
                                    disabledStats.Add(i);
                                    return true;
                            });

        if(item is Weapon)
        {
            //Parse the weapon stats here
        }

        foreach(Item.ItemStats stat in item.stats)
        {
            //Generate The stats for oldmate
            if(disabledStats.Count == 0)
            {   
                Debug.Log("Item has too many stats!");
                return;
            }

            TooltipStat t = disabledStats[0];
            disabledStats.Remove(t);
            t.gameObject.SetActive(true);
            t.Setup(stat);
            curStats.Add(t);
        }
    }
    public void Setup(Item oldItem, Item item)
    {
        IEnumerable<Item.ItemStats> missing = oldItem.stats.Where(i => !item.stats.Any(e => e.stat != i.stat));

        Setup(item);
    }
}
