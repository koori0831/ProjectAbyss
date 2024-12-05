using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _entityMover.CanManualMove = false;
    }

    public override void Exit()
    {
        _entityMover.CanManualMove = true;
        base.Exit();
    }
}
