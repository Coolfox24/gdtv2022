using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : BaseState
{
    protected BossStateMachine stateMachine;

    public BossBaseState(BossStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }


    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnTick(float deltaTime)
    {
    }
}
