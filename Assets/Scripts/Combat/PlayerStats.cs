using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public enum PLAYER_STATS{
        SPEED,
        COOLDOWN,
        DAMAGE,
        DAMAGE_REDUCTION,
        PROJECTILE_AMOUNT,
        PROJECTILE_PASSTHROUGH,
        AREA,
        EXTRA_SWINGS,
        SWING_SPEED,
        LUCK,
        INVINCIBILITY_TIME,
        PROJECTILE_SPEED
    }

    [SerializeField] public float Speed; //Movement Speed
    [SerializeField] public float Cooldown; //Reduces cooldown of weapons
    [SerializeField] public float Damage; //Increases damage of weapons
    [SerializeField] public float Damage_Reduction; //Flat damage reduction from attacks
    [SerializeField] public int Proj_amount; //Amount of projectiles fired
    [SerializeField] public float Proj_passthrough; //Amount of enemies projectiles will pass through
    [SerializeField] public float Proj_speed;
    [SerializeField] public float Area; //Size of projectile & swing area
    [SerializeField] public float Extra_Swings; //Extra swings of melee based weapons
    [SerializeField] public float Swing_Speed;
    [SerializeField] public float Luck; //Influences chance for items to drop and rarity -- Item Weights > 30 - Luck < 30 + luc6k
    [SerializeField] public float IFrames; //Amount of invulnerability frames player has after being hit

    public PlayerStats()
    {
        //Randomize the values a bit here
        Speed = Random.Range(4, 7);
        Cooldown = Random.Range(0, 3);
        Damage = Random.Range(0, 4);
        Proj_amount = Random.Range(0, 2);
        Proj_passthrough = Random.Range(0, 3);
        Extra_Swings = Random.Range(0, 2);
        Swing_Speed = Random.Range(0, 3);
    }
}
