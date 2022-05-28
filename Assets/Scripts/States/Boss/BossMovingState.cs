using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovingState : BossBaseState
{
    float moveSpeed = 4.5f;
    float timeToMove = 2f;
    float moveTime = 0f;
    Vector2 moveTo;
    Vector2 startingPos;
    public BossMovingState(BossStateMachine stateMachine, Vector2 movePos) : base(stateMachine)
    {
        moveTo = movePos;
        startingPos = stateMachine.transform.position;

        Vector2 t = moveTo - startingPos;
        if(t.x < 1)
        {
            stateMachine.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            stateMachine.transform.localScale = new Vector3(-1, 1, 1);
        }
        timeToMove = t.magnitude / moveSpeed;
    }

    public override void OnEnter()
    {
        stateMachine.animator.SetBool(stateMachine.BossMovingHash, true);
    }

    public override void OnTick(float deltaTime)
    {
        moveTime += deltaTime;
        stateMachine.body.MovePosition(Vector2.Lerp(startingPos, moveTo, moveTime / timeToMove));

        if(moveTime > timeToMove)
        {
            stateMachine.SwitchState(new BossIdleState(stateMachine, Random.Range(2f, 6f)));
        }
    }

    public override void OnExit()
    {
        stateMachine.animator.SetBool(stateMachine.BossMovingHash, false);
    }
}
