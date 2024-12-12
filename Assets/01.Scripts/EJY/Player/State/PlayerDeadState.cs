using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
