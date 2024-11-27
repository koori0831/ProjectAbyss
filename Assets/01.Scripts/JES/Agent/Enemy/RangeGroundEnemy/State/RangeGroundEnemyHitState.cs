using UnityEngine;

public class RangeGroundEnemyHitState : State<RangeGroundEnemyStateType>
{
    public RangeGroundEnemyHitState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
