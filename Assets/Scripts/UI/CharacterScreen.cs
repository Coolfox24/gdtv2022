using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreen : MonoBehaviour
{
    public List<CharacterSlot> slots;

    public TooltipStat Health;
    public TooltipStat Time;
    public TooltipStat Speed;
    public TooltipStat Cooldown;
    public TooltipStat DMG;
    public TooltipStat Armor;
    public TooltipStat ExtraProj;
    public TooltipStat Pierce;
    public TooltipStat Area;
    public TooltipStat ExtraSwings;
    public TooltipStat SwingSpeed;
    public TooltipStat Luck;
    public TooltipStat IFrames;

    void UpdateCharStats()
    {
        PlayerStateMachine psm = FindObjectOfType<PlayerStateMachine>();
        Health h = psm.GetComponent<Health>();
        Health.statAmount.text = h.curHealth + "/" + h.maxHealth;
        Time.statAmount.text = Mathf.FloorToInt(psm.timeRemaining).ToString(); //Will need to round this most likely
        Speed.statAmount.text = (psm.PlayerStats.Speed + psm.Equipment.armorStats.Speed).ToString();
        Cooldown.statAmount.text = (psm.PlayerStats.Cooldown + psm.Equipment.armorStats.Cooldown).ToString();
        DMG.statAmount.text = (psm.PlayerStats.Damage + psm.Equipment.armorStats.Damage).ToString();
        Armor.statAmount.text = (psm.PlayerStats.Damage_Reduction + psm.Equipment.armorStats.Damage_Reduction).ToString();
        ExtraProj.statAmount.text = (psm.PlayerStats.Proj_amount + psm.Equipment.armorStats.Proj_amount).ToString();
        Pierce.statAmount.text = (psm.PlayerStats.Proj_passthrough + psm.Equipment.armorStats.Proj_passthrough).ToString();
        Area.statAmount.text = (psm.PlayerStats.Area + psm.Equipment.armorStats.Area).ToString();
        ExtraSwings.statAmount.text = (psm.PlayerStats.Extra_Swings + psm.Equipment.armorStats.Extra_Swings).ToString();
        SwingSpeed.statAmount.text = (psm.PlayerStats.Swing_Speed + psm.Equipment.armorStats.Swing_Speed).ToString();
        Luck.statAmount.text = (psm.PlayerStats.Luck + psm.Equipment.armorStats.Luck).ToString();
        IFrames.statAmount.text = (psm.PlayerStats.IFrames + psm.Equipment.armorStats.IFrames).ToString();
    }

    public void OnShow()
    {
        this.gameObject.SetActive(true);
        UpdateCharStats();
        foreach (CharacterSlot slot in slots)
        {
            slot.OnShow();
        }
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}
