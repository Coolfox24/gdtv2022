using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreen : MonoBehaviour
{
    public List<CharacterSlot> slots;

    public void OnShow()
    {
        this.gameObject.SetActive(true);

        foreach(CharacterSlot slot in slots)
        {
            slot.OnShow();
        }
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
