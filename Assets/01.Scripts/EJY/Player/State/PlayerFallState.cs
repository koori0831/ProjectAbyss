using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void StateFixedUpdate()
    {
        if (_entityMover.isGround.Value)
            _stateMachine.ChangeState(PlayerStateEnum.PlayerIdle);

        base.StateFixedUpdate();
    }
}
