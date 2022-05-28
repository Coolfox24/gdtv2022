using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyBaseState
{
    float cooldownRemaining;
    bool charging;

    Vector2 dir;

    public EnemyChargeState(EnemyStateMachine esm) : base(esm)
    {
        cooldownRemaining = esm.Stats.specialCooldown;
    }

    public override void OnTick(float deltaTime)
    {
        if(charging)
        {
            Vector2 currentPos = stateMachine.body.position;
            Vector2 adjustedMovement = dir * stateMachine.Stats.moveSpeed; 
            Vector2 newPos = currentPos - adjustedMovement * deltaTime;
            stateMachine.body.MovePosition(newPos);
            if((cooldownRemaining -= deltaTime) < 0)
            {
                charging = false;
                cooldownRemaining = stateMachine.Stats.specialCooldown;
            }
            return;
        }

        if((cooldownRemaining -= deltaTime) <= 0)
        {
            cooldownRemaining = stateMachine.Stats.specialCooldown;
            charging = true;

            dir = stateMachine.transform.position - stateMachine.playerPos.position;
            dir.Normalize();    

            stateMachine.animator.SetFloat(stateMachine.EnemyDirectionXHash, -dir.x);
            stateMachine.animator.SetFloat(stateMachine.EnemyDirectionYHash, -dir.y);
        }
    }
}

