using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    [field: SerializeField] public InputHandler Input {get; private set;}
    [field: SerializeField] public PlayerEquipment Equipment {get; private set;}
    [field: SerializeField] public PlayerStats PlayerStats {get; private set;}
    [field: SerializeField] public Animator Animator {get; private set;}
    [field: SerializeField] public FootprintSpawner FootprintSpawner {get; private set;}
    [field: SerializeField] public CharacterScreen CharScreen {get; private set;}
    [field: SerializeField] public ItemSelection ItemSelection {get; private set;}
    [field: SerializeField] public HUDController HUD {get; private set;}
    [field: SerializeField] public LootTableSO StartingWeapon {get; private set;}

    BossStateMachine death;

    float maxTime = 60 * 20;
    float timeRemaining = 60 * 20;

    public int PlayerMovingHash = Animator.StringToHash("IsMoving");
    public int PlayerDirectionXHash = Animator.StringToHash("X");
    public int PlayerDirectionYHash = Animator.StringToHash("Y");


    public float iFrames = 0f;

    private void Start()
    {
        death = FindObjectOfType<BossStateMachine>();
        death.gameObject.SetActive(false);
        SwitchState(new PlayerMovementState(this));
        PlayerStats = new PlayerStats(); //Can use this to generate random starting stats for player
        //Randomize Weapon
        Equipment.ChangeWeapon(new Weapon((WeaponSO)StartingWeapon.GetDroppedItem(), Item.Rarity.common), 0);
    }

    private void FixedUpdate()
    {
        if(iFrames >= 0)
        {
            iFrames -= Time.deltaTime;
        }
        curState?.OnTick(Time.deltaTime);

        timeRemaining -= Time.deltaTime;
        HUD.UpdateTime((int)Mathf.Ceil(20f * (timeRemaining / maxTime)));
        if(timeRemaining < 0 && !alreadyDied)
        {
            OnDie(); //Start end fight when time runs out
        }
    } 
    bool alreadyDied = false;
    public override void OnDie()
    {
        //Change state here to death state

        //Will have to call some function on the spawner to progress to death mode
        Debug.Log("Dead");

        if(alreadyDied)
        {
            //Gameover -- Show gameover screen
            //Death animation
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            alreadyDied = true;
            //Fade to black
            transform.position = new Vector3(-375, 222, 0);
            timeRemaining = 0;
            death.gameObject.SetActive(true);
            //Start death music etc.
        }
        
        //Spawn Death
    }

    //Own Take Dmg function to allow iframes
    public void TakeDmg(float amount)
    {
        if(iFrames > 0)
        {
            return;
        }
        if(amount < Equipment.armorStats.Damage_Reduction)
        {
            return; //Prevents us healing from dmg - also won't trigger i frames on hits that deal no dmg
        }

        iFrames = 0.2f + ((PlayerStats.IFrames + Equipment.armorStats.IFrames) * 0.2f); //TODO change this to a stat
        //Apply Armor or DR to attack here
        amount -= (PlayerStats.Damage_Reduction + Equipment.armorStats.Damage_Reduction);
        Health hp = gameObject.GetComponent<Health>();
        hp.TakeDamage(amount);

        HUD.UpdateHealth( (int)Mathf.Ceil((hp.curHealth / hp.maxHealth) * 20));
    }

}
