using System;
using UnityEngine;

public class PlayerClimbState : PlayerState
{
    public PlayerClimbState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _entityMover.StopImmediately(true);
        _entityMover.SetGravityScale(0);

        _playerInput.JumpEvent += FallFromWall;
    }

    private void FallFromWall()
    {
        _player.StateMachine.ChangeState(PlayerStateEnum.PlayerFall);
    }

    public override void Exit()
    {
        _entityMover.SetGravityScale(1);
        base.Exit();
    }
}
