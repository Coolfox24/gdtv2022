using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : BaseState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine esm)
    {
        stateMachine = esm;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnTick(float deltaTime)
    {
        //Move Towards Player
        Move(deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Vector2 dir = stateMachine.transform.position - stateMachine.playerPos.position;
        //Check if too far away from player and despawn if so
        if(dir.sqrMagnitude > stateMachine.MaxDistanceFromPlayerSqrd)
        {
            stateMachine.OnDie();
            return;
        }
        dir.Normalize();
        //Can limit this calc if its too harsh on performance later

        stateMachine.animator.SetFloat(stateMachine.EnemyDirectionXHash, -dir.x);
        stateMachine.animator.SetFloat(stateMachine.EnemyDirectionYHash, -dir.y);

        Vector2 currentPos = stateMachine.body.position;
        Vector2 adjustedMovement = dir * stateMachine.Stats.moveSpeed; //TODO Change the 1 to be enemy movepseed
        Vector2 newPos = currentPos - adjustedMovement * deltaTime;
        stateMachine.body.MovePosition(newPos);
    }
}
