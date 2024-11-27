using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _entityMover.AddForceToEntity(new Vector2(0, _player.jumpPower));
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        if (_entityMover.Velocity.y < 0)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerFall);
    }
}
