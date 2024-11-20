public class EnemyDeathState : State<EnemyStateType>
{
    public EnemyDeathState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
    }
}