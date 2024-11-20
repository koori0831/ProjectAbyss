using UnityEngine;

public class EnemyHitState : State<EnemyStateType>
{
    public EnemyHitState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
