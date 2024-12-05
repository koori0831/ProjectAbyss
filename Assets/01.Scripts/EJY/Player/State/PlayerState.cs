using System;
using UnityEngine;

public class PlayerState : State<PlayerStateEnum>
{
    protected Player _player;
    protected PlayerInputSO _playerInput;
    public PlayerState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
        _player = entity as Player;
        _playerInput = _player.GetPlayerCompo<PlayerInputSO>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerInput.AttackEvent += HandleAttackEvent;
        _renderer.OnAnimationEnd += AnimationEndTrigger;
    }

    protected virtual void HandleAttackEvent()
    {
        _player.StateMachine.ChangeState(PlayerStateEnum.PlayerAttack);
    }

    public override void Exit()
    {
        _playerInput.AttackEvent -= HandleAttackEvent;
        _renderer.OnAnimationEnd -= AnimationEndTrigger;
        base.Exit();
    }
}
