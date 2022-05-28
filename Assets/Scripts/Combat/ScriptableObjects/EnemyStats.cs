using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy Stats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public float ScalingFactor; //Scales with time by this much per minute after minute stated below? 0.1f = 10% extra stats every minute
    public int TimeToStartScaling; //Minute to start the scaling at -- allows easier mobs to scale slower
    public float MaxHealth;
    public float moveSpeed;
    public float Dmg;
    public float ExpGiven;
    public AISettings AIMode;
    public LootTableSO LootTable;
    public float specialCooldown;
    public GameObject specialProjectile;

    public enum AISettings
    {
        Standard,
        Caster, //Fires homing projectiles
        Charger,//Only moves by charging
        Archer, //Fires shotgun projectiles
        Totem   //Causes an aoe around it
    }
}
