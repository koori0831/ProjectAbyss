using UnityEngine;

public class EnemyIdleState : State<EnemyStateType>
{
    public EnemyIdleState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
    
}
