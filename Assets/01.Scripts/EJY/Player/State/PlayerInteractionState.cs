using UnityEngine;

public class PlayerInteractionState : PlayerState
{
    public PlayerInteractionState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
