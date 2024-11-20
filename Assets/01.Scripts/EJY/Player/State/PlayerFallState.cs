using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
