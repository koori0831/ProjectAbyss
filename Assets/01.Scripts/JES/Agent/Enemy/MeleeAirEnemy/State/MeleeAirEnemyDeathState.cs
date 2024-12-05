public class MeleeAirEnemyDeathState : State<MeleeAirEnemyStateType>
{
    private Enemy _enemy;

    public MeleeAirEnemyDeathState(Entity entity, string animaName, StateMachine<MeleeAirEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as Enemy;
    }
    public override void Enter()
    {
        base.Enter();
        _enemy.IsDead = true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if(_isTriggerCall)
            _enemy.DestroyEnemy();
    }
}