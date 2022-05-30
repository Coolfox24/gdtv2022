using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : LootGenerator
{
    public LootTableSO PotentialLoot;

    public Item item;

    void Start()
    {
        if(PotentialLoot == null)
        {
            Debug.Log("Set a loot table for chest in scene at" + transform.position);
            return;
        }
        ItemScriptableObject i = GenerateItem(PotentialLoot);
        Item.Rarity r = GenerateRarity(PotentialLoot);


        if(i is WeaponSO)
        {
            item = new Weapon((WeaponSO)i, r);
        }
        else
        {
            item = new Armor((ArmorItemSO)i, r);
        }
        //Generate Random stats for item here
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }
        //Change state to Item Selection State
        //Would need to freeze all enemies as well
        PlayerStateMachine psm = other.GetComponent<PlayerStateMachine>();
        psm.SwitchState(new PlayerItemSelectionState(psm, item));

        Destroy(this.gameObject);
    }
}
