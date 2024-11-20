public class EnemyAttackState : State<EnemyStateType>
{
    public EnemyAttackState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}