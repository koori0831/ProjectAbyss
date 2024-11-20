using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
