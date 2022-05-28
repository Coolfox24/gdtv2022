using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;

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
