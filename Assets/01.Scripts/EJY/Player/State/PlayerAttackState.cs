using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Entity entity, string animaName, StateMachine<PlayerStateEnum> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
