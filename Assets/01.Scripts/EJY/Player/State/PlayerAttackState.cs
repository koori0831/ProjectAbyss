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

        _entityMover.CanManualMove = false;
        _entityMover.StopImmediately();
        _entityMover.SetVelocity(new Vector2(_renderer.FacingDirection * 3, 0));
        _player.canFlip = false;
        _weaponHolder.TrailRenderer.enabled = true;
    }

    protected override void HandleAttackEvent()
    {
    }

    public override void StateUpdate()
    {
        if (_isTriggerCall)
            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerIdle);
    }

    public override void Exit()
    {
        _entityMover.CanManualMove = true;
        _player.canFlip = true;
        _weaponHolder.TrailRenderer.enabled = false;
        base.Exit();
    }
}
