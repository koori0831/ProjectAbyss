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
    }

    public override void Exit()
    {
        _player.canFire = true;
        base.Exit();

    }
}
