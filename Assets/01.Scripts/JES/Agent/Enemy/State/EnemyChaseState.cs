using UnityEngine;

public class EnemyChaseState : State<EnemyStateType>
{
    public EnemyChaseState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}
