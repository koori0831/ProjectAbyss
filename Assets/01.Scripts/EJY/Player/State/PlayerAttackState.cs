using System;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.OnAttackEvent?.Invoke();
    }

    public override void StateFixedUpdate()
    {
        float movementX = _playerInput.InputDirection.x;

        _entityMover.SetXMovement(movementX);
    }
}
