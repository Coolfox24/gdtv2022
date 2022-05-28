using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{
    public abstract void OnDie();
    protected BaseState curState;
    
    public void SwitchState(BaseState state)
    {
        curState?.OnExit();
        curState = state;
        state.OnEnter();
    }

}
