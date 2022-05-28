using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2State : BossBaseState
{
    public BossAttack2State(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    AnimatorClipInfo[] curClipInfo;

    public override void OnEnter()
    {
        stateMachine.animator.SetTrigger(stateMachine.BossAttack2Hash);
    }

    public override void OnTick(float deltaTime)
    {
        //if(stateMachine.animator.GetCurrentAnimatorClipInfo(0))
        curClipInfo = stateMachine.animator.GetCurrentAnimatorClipInfo(0);
        if(curClipInfo[0].clip.name != "Attack2")
        {
            stateMachine.SwitchState(new BossIdleState(stateMachine, 5f));
        }
    }

    public override void OnExit()
    {
        //Spawn one on the players location
        GameObject.Instantiate(stateMachine.Attack2, stateMachine.playerPos.position, Quaternion.identity);

        for(int i = 0; i < 6; i++)
        {
            Vector2 offset = new Vector2(Random.Range(-3, 3f), Random.Range(-4, 4f));
            Vector2 position = stateMachine.playerPos.position;

            GameObject.Instantiate(stateMachine.Attack2, position + offset, Quaternion.identity);
        }
    }
}
