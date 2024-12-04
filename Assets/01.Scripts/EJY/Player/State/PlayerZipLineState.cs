using UnityEngine;

public class PlayerZipLineState : PlayerState
{
    public PlayerZipLineState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.canFire = false;
        _entityMover.SetGravityScale(0);
    }

    public override void Exit()
    {
        _player.canFire = true;
        _entityMover.SetGravityScale(1);
        base.Exit();

    }
}
