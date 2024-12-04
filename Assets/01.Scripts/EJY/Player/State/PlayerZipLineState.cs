using UnityEngine;

public class PlayerZipLineState : PlayerState
{
    public PlayerZipLineState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
}
