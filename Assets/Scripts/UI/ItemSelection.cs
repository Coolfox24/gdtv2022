using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    public PlayerStateMachine PSM;
        
    public GameObject ReplaceItemContainer;
    public ItemTooltip CItemTooltip;
    public ItemTooltip NItemTooltip;
    public GameObject Buttons;
    public GameObject OnlyTakeItemContainer;
    public ItemTooltip OTItemTooltip;

    Item Item;
    Item cItem;
    int curItemIndex;

    public void Setup(Item newItem)
    {
        Item = newItem;
        Buttons.SetActive(false);

        if(Item is Armor)
        {
            Armor AItem = (Armor)Item;
            switch(AItem.slot)
            {
                case ArmorItemSO.ARMOR_SLOTS.HEAD:
                    cItem = PSM.Equipment.helmet;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.CHEST:
                    cItem = PSM.Equipment.body;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.LEGS:
                    cItem = PSM.Equipment.legs;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.ACCESSORY:
                    if(PSM.Equipment.access1 == null)
                    {
                        Debug.Log(1);
                        curItemIndex = 0;
                        cItem = null;
                    }
                    else if(PSM.Equipment.access2 == null)
                    {
                        Debug.Log(2);
                        curItemIndex = 1;
                        cItem = null;
                    }
                    else
                    {
                        Debug.Log(0);
                        curItemIndex = 0;
                        cItem = PSM.Equipment.access1;
                    }
                    if(cItem != null)
                    {
                        Buttons.SetActive(true);
                    } //Better logic on this
                    break;
                
            }
        }
        else
        {
            Weapon wep = (Weapon) Item;
            cItem = PSM.Equipment.currentWeapons[0]; //Will always have a starting weapon
            curItemIndex = 0;
            bool showScreen = true;
            for(int i = 0 ; i < PSM.Equipment.currentWeapons.Count; i++)
            {
                Debug.Log(Item.itemName + " = " + PSM.Equipment.currentWeapons[i]);
                if(PSM.Equipment.currentWeapons[i] == null)
                {
                    curItemIndex = i;
                    cItem = null;
                    showScreen = false;
                    break;
                }
                else if(PSM.Equipment.currentWeapons[i].itemName.Equals(Item.itemName) || 
                        PSM.Equipment.currentWeapons[i].WeaponCategory.Equals(wep.WeaponCategory)) //Check if they share same category - ie only 1 sword
                {
                    curItemIndex = i; //To replace current item
                    cItem = PSM.Equipment.currentWeapons[i];
                    showScreen = false;
                    break;
                }
            }
            if(showScreen) //We have all weapon slots filled and not a specific weapon to replace
            {
                Buttons.SetActive(true);
            }
        }

        Debug.Log(cItem);
        if(NoItemCheck(cItem))
        {
            //Display a single Tooltip here with a button to take
            OTItemTooltip.Setup(Item);

            OnlyTakeItemContainer.SetActive(true);
            ReplaceItemContainer.SetActive(false);
        }
        else
        {
            CItemTooltip.Setup(cItem);
            NItemTooltip.Setup(cItem, Item);

            OnlyTakeItemContainer.SetActive(false);
            ReplaceItemContainer.SetActive(true);
        }
        this.gameObject.SetActive(true);
    }

    private bool NoItemCheck(Item item)
    {
        //Check here if we currently have that item 
        if(item == null)
        {
            return true;
        } 
        return false;
    }

    public void NextItem()
    {
        curItemIndex ++;

        if(Item is Armor)
        {
            if(curItemIndex > 1)
            {
                curItemIndex = 0;
            }

            if(curItemIndex == 0)
            {
                cItem = PSM.Equipment.access1;
            }
            else
            {
                cItem = PSM.Equipment.access2;
            }
        }
        else
        {
            if(curItemIndex >= 5)
            {
                curItemIndex = 0;
            }

            cItem = PSM.Equipment.currentWeapons[curItemIndex];
        }

        CItemTooltip.Setup(cItem);
        NItemTooltip.Setup(cItem, Item);
    }

    public void PrevItem()
    {
        if(Item is Armor)
        {
            NextItem();
            return;
        }
        curItemIndex --;

        if(curItemIndex < 0)
        {
            curItemIndex = 4;

            cItem = PSM.Equipment.currentWeapons[curItemIndex];
        }
        CItemTooltip.Setup(cItem);
        NItemTooltip.Setup(cItem, Item);
    }

    public void ReplaceItem()
    {
        if(Item is Armor)
        {
            Armor aItem = (Armor)Item;
            PSM.Equipment.ChangeItem(aItem, aItem.slot, curItemIndex);
        }
        else
        {
            PSM.Equipment.ChangeWeapon((Weapon)Item, curItemIndex);
        }
        OnClose();
    }

    public void DiscardItem()
    {
        PSM.GetComponent<Health>().AddMaxHealth(5);
        OnClose();
    }

    public void TakeItem()
    {

        if(Item is Armor)
        {
            Armor aItem = (Armor) Item; 
            PSM.Equipment.ChangeItem(aItem, aItem.slot, curItemIndex);
        }
        else
        {
            PSM.Equipment.ChangeWeapon((Weapon)Item, curItemIndex);
        }

        OnClose();
    }

    private void OnClose()
    {
        this.gameObject.SetActive(false);
        OnlyTakeItemContainer.SetActive(false);
        ReplaceItemContainer.SetActive(false);

        PSM.SwitchState(new PlayerMovementState(PSM));
        Time.timeScale = 1;
    }
}
