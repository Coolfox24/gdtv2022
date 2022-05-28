using UnityEngine;

public class PlayerViewingMenuState : PlayerBaseState
{
    bool firstFrame = true;

    public PlayerViewingMenuState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnEnter()
    {
        stateMachine.GetComponent<EnemySpawner>().PauseGame();
        stateMachine.Input.Menu += OnCloseMenu;
        stateMachine.CharScreen.OnShow();
        stateMachine.Animator.SetBool(stateMachine.PlayerMovingHash, false);
        
        Time.timeScale = 0;
    }

    public override void OnExit()
    {
        stateMachine.GetComponent<EnemySpawner>().PauseGame();
        stateMachine.Input.Menu -= OnCloseMenu;
    }

    public override void OnTick(float deltaTime)
    {
        if(firstFrame)
        {
            firstFrame = false;
        }
    }

    //Would need to register other events in here to allow controllers and pure keykoard to view items and what not
    public void OnCloseMenu()
    {
        // if(firstFrame)
        // {
        //     return;
        // }
        Time.timeScale = 1;
        stateMachine.CharScreen.OnHide();
        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
        stateMachine.GetComponent<EnemySpawner>().UnpauseGame();
    }

}
