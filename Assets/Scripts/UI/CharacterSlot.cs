using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerEquipment equipment;
    public ItemTooltip ItemToolTip;
    public bool IsArmor;
    public ArmorItemSO.ARMOR_SLOTS slot;
    public int WeaponIndex;

    public Image icon;

    public void OnShow()
    {
        if (IsArmor)
        {
            switch (slot)
            {
                case ArmorItemSO.ARMOR_SLOTS.HEAD:
                    icon.sprite = equipment.helmet == null ? icon.sprite : equipment.helmet.icon;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.CHEST:
                    icon.sprite = equipment.body == null ? icon.sprite : equipment.body.icon;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.LEGS:
                    icon.sprite = equipment.legs == null ? icon.sprite : equipment.legs.icon;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.ACCESSORY:
                    if (WeaponIndex == 0)
                    {
                        icon.sprite = equipment.access1 == null ? icon.sprite : equipment.access1.icon;
                    }
                    else
                    {
                        icon.sprite = equipment.access2 == null ? icon.sprite : equipment.access2.icon;
                    }
                    break;

            }
        }
        else
        {
            if(equipment.currentWeapons[WeaponIndex] != null)
            {
                icon.sprite = equipment.currentWeapons[WeaponIndex] == null ? icon.sprite : equipment.currentWeapons[WeaponIndex].icon;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item curItem = null;
        if (IsArmor)
        {
            switch (slot)
            {
                case ArmorItemSO.ARMOR_SLOTS.HEAD:
                    curItem = equipment.helmet;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.CHEST:
                    curItem = equipment.body;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.LEGS:
                    curItem = equipment.legs;
                    break;
                case ArmorItemSO.ARMOR_SLOTS.ACCESSORY:
                    if (WeaponIndex == 0)
                    {
                        curItem = equipment.access1;
                    }
                    else
                    {
                        curItem = equipment.access2;
                    }
                    break;

            }
        }
        else
        {
            curItem = equipment.currentWeapons[WeaponIndex];
        }

        if (curItem == null)
        {
            return;
        }
        ItemToolTip.Setup(curItem);
        ItemToolTip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemToolTip.gameObject.SetActive(false);
    }
}
