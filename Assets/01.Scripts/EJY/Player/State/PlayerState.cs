using System;
using UnityEngine;

public class PlayerState : State<PlayerStateEnum>
{
    protected Player _player;
    protected PlayerInputSO _playerInput;
    protected WeaponHolder _weaponHolder;
    public PlayerState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
        _player = entity as Player;
        _playerInput = _player.GetPlayerCompo<PlayerInputSO>();
        _weaponHolder = _player.GetPlayerCompo<WeaponHolder>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerInput.AttackEvent += HandleAttackEvent;
        _renderer.OnAnimationEnd += AnimationEndTrigger;
    }

    protected virtual void HandleAttackEvent()
    {
        if (_weaponHolder.Weapon.gameObject.activeSelf)
        _player.StateMachine.ChangeState(PlayerStateEnum.PlayerAttack);
    }

    public override void Exit()
    {
        _playerInput.AttackEvent -= HandleAttackEvent;
        _renderer.OnAnimationEnd -= AnimationEndTrigger;
        base.Exit();
    }
}
