using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1State : BossBaseState
{
    public BossAttack1State(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        stateMachine.animator.SetTrigger(stateMachine.BossAttack1Hash);
    }

    public override void OnTick(float deltaTime)
    {

    }
}
