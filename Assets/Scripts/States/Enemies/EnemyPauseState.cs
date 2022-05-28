using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPauseState : BaseState
{
    EnemyStateMachine stateMachine;
    public EnemyPauseState(EnemyStateMachine esm) : base()
    {
        this.stateMachine = esm;
    }

    public override void OnEnter()
    {
        //Stop All Movement
        stateMachine.body.velocity = Vector2.zero;        
    }

    public override void OnExit()
    {

    }

    public override void OnTick(float deltaTime)
    {
        //Don't Do Nothing
    }
}
