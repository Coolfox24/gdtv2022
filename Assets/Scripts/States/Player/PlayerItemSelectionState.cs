using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerItemSelectionState : PlayerBaseState
{
    EnemySpawner spawner;
    Item newItem;

    public PlayerItemSelectionState(PlayerStateMachine stateMachine, Item item) : base(stateMachine)
    {
        // Store item here and use this to show UI elements
        //Add functions to this to this to do something regarding the UI

        newItem = item;

    }

    public override void OnEnter()
    {
        //Freeze all enemies & Pause Spawner
        spawner = stateMachine.GetComponent<EnemySpawner>();
        spawner.PauseGame();

        stateMachine.Input.Confirm += OnConfirm;    

        //Tell Animator to stop moving
        stateMachine.Animator.SetBool(stateMachine.PlayerMovingHash, false);

        stateMachine.ItemSelection.Setup(newItem);

        Time.timeScale = 0;
    }

    public override void OnExit()
    {
        spawner.UnpauseGame();

        stateMachine.Input.Confirm -= OnConfirm;
        Time.timeScale = 1;
    }

    public override void OnTick(float deltaTime)
    {

    }

    public void OnConfirm()
    {
        //Need to get the current button and  do stuff with it 
        
    }
}
