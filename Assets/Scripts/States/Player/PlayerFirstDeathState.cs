using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFirstDeathState : PlayerBaseState
{
    float timer = 2f;
    float curTime = 0f;
    bool fadeIn = false;
    Color curColor;

    public PlayerFirstDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        curColor = stateMachine.BlackScreen.color;
        stateMachine.BlackScreen.gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        stateMachine.BlackScreen.gameObject.SetActive(false);
    }

    public override void OnTick(float deltaTime)
    {
        curTime += deltaTime;

        if(!fadeIn)
        {
            //increase alpha of black screen
            curColor.a = 1 * (curTime / timer);
            stateMachine.BlackScreen.color = curColor;
            if(curTime >= timer)
            {
                fadeIn = true;
                curTime = 0;
                stateMachine.SetupDeath();
                //move player here
            }
        }
        else
        {
            //decrease alpha
            curColor.a = 1 - (1 * (curTime/timer));
            stateMachine.BlackScreen.color = curColor;
            if(curTime >= timer)
            {
                stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            }
        }
    }
}
