using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
    public override void StateUpdate()
    {
        if (_playerInput.InputDirection.x == 0)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerIdle);
    }

    public override void StateFixedUpdate()
    {
        _entityMover.SetXMovement(_playerInput.InputDirection.x);
    }
}
