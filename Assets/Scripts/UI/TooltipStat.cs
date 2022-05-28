using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipStat : MonoBehaviour
{
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statAmount;

    public void Setup(Item.ItemStats stats)
    {
        switch(stats.stat)
        {
            case PlayerStats.PLAYER_STATS.PROJECTILE_AMOUNT:
                statName.text = "Extra Proj";
                break;
            case PlayerStats.PLAYER_STATS.SPEED:
                statName.text = "Movespeed";
                break;
            case PlayerStats.PLAYER_STATS.PROJECTILE_PASSTHROUGH:
                statName.text = "Pierce";
                break;
            case PlayerStats.PLAYER_STATS.DAMAGE_REDUCTION:
                statName.text = "Armor";
                break;
            default:
                statName.text = stats.stat.ToString().Replace("_", " "); 
                break;
        }
        statAmount.text = stats.amount.ToString();
    }

    public void Setup(WeaponSO wep)
    {
        //Do Weapon stats here
    }
}
