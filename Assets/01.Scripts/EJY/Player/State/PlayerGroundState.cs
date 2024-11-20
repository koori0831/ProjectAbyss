using System;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _playerInput.JumpEvent += HandleJumpEvent;
    }

    public override void StateFixedUpdate()
    {
        if(_entityMover.isGround.Value == false)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerFall);
    }

    private void HandleJumpEvent()
    {
        if (_entityMover.isGround.Value)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerJump);
    }

    public override void Exit()
    {
        _playerInput.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }
}
