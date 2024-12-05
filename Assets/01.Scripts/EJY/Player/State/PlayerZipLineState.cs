using UnityEngine;

public class PlayerZipLineState : PlayerState
{
    public PlayerZipLineState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.canAttack = false;
        _entityMover.SetGravityScale(0);
    }

    public override void Exit()
    {
        _player.canAttack = true;
        _entityMover.SetGravityScale(1);
        base.Exit();

    }
}
