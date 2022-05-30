using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    float idleTimeRemaining;

    public BossIdleState(BossStateMachine stateMachine, float idleTime) : base(stateMachine) 
    {
        idleTimeRemaining = idleTime;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnTick(float deltaTime)
    {
        //face player
        Vector2 t = stateMachine.transform.position - stateMachine.playerPos.position;
        if(t.x < 1)
        {
            stateMachine.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            stateMachine.transform.localScale = new Vector3(1, 1, 1);
        }

        if((idleTimeRemaining -= deltaTime) < 0)
        {
            //enter into a different state here
            float rng = Random.Range(0f, 1f);
            if(rng < 0.5f)
            {
                //move
                stateMachine.SwitchState(new BossMovingState(stateMachine, movePos())); //Need to pick a random number here to move to
            }
            else if(rng < 0.75f)
            {
                stateMachine.SwitchState(new BossAttack1State(stateMachine));
            }
            else 
            {
                stateMachine.SwitchState(new BossAttack2State(stateMachine));
            }
        }
    }

    private Vector2 movePos()
    {
        //Distance we want to be from the player
        float xOffset = Random.Range(4, 10) * (Random.Range(0, 2) == 0 ? 1 : -1);
        float yOffset = Random.Range(4, 7) * (Random.Range(0, 2) == 0 ? 1 : -1);
        Vector2 pos = new Vector2(stateMachine.playerPos.position.x + xOffset, stateMachine.playerPos.position.y + yOffset);
        return pos;
    }
}

