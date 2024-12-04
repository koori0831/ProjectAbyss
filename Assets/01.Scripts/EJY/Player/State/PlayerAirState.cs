using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void StateFixedUpdate()
    {
        if (_entityMover.isGround.Value)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerIdle);

        float x = _playerInput.InputDirection.x;

        if(Mathf.Abs(x) > 0)
        _entityMover.SetXMovement(x);

        else
            _entityMover.StopImmediately();
    }
}
