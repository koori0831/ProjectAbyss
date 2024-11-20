using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _entityMover.StopImmediately();
    }

    public override void StateUpdate()
    {
        if (Mathf.Abs(_playerInput.InputDirection.x) > 0)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerMove);
    }
}
