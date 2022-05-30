using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    Rigidbody2D body;


    private float timeSinceLastFootprint;
    const float footprintTime = 0.2f;

    public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        body = stateMachine.GetComponent<Rigidbody2D>(); //Set initial projectile direction
    }

    public override void OnEnter()
    {
        timeSinceLastFootprint = footprintTime;

        stateMachine.Input.Menu += ShowCharacterScreen;

    }
    
    public override void OnTick(float deltaTime)
    {
        Move(deltaTime);
        CalculateFacing();
        FireWeapons(deltaTime);
    }

    private void SpawnFootprint(float deltaTime, Vector2 position)
    {
        if(stateMachine.Input.Movement.sqrMagnitude == 0)
        {
            return;
        }
        if((timeSinceLastFootprint-=deltaTime) < 0)
        {
            timeSinceLastFootprint = footprintTime;
            stateMachine.FootprintSpawner.SpawnFootprint(stateMachine.Facing, position);
        }
    }

    public override void OnExit()
    {
        stateMachine.Input.Menu -= ShowCharacterScreen;
    }

    private void Move(float deltaTime)
    {
        Vector2 currentPos = body.position;
        Vector2 adjustedMovement = stateMachine.Input.Movement * (3 + ((stateMachine.PlayerStats.Speed + stateMachine.Equipment.armorStats.Speed)/5));
        Vector2 newPos = currentPos + adjustedMovement * deltaTime;
        SpawnFootprint(deltaTime, newPos);
        body.MovePosition(newPos);
    }

    private void CalculateFacing()
    {
        float x = stateMachine.Input.Movement.x;
        float y = stateMachine.Input.Movement.y;

        if(x != 0 || y != 0)
        {
            stateMachine.Facing = stateMachine.Input.Movement;
            stateMachine.Animator.SetFloat(stateMachine.PlayerDirectionXHash, x);
            stateMachine.Animator.SetFloat(stateMachine.PlayerDirectionYHash, y);
            stateMachine.Animator.SetBool(stateMachine.PlayerMovingHash, true);
        }
        else
        {
            stateMachine.Animator.SetBool(stateMachine.PlayerMovingHash, false);
        }
    }

    private void FireWeapons(float deltaTime)
    {
        foreach(Weapon weapon in stateMachine.Equipment.currentWeapons)
        {
            if(weapon == null)
            {
                //Should be no weapons after this to fire
                return;
            }
            weapon?.CheckWeaponCooldown(deltaTime,  stateMachine.Facing, stateMachine.PlayerStats, stateMachine.Equipment.armorStats, stateMachine.transform);
        }
    }

    private void ShowCharacterScreen()
    {
        //Do this in a new state rather than here -- just use this to transition to new state
        stateMachine.SwitchState(new PlayerViewingMenuState(stateMachine));
    }
}
