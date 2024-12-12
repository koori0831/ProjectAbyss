public class MeleeAirEnemyDeathState : State<MeleeAirEnemyStateType>
{
    private Enemy _enemy;

    public MeleeAirEnemyDeathState(Entity entity, string animaName, StateMachine<MeleeAirEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as Enemy;
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_isTriggerCall)
        {
            _stateMachine.ChangeState(MeleeAirEnemyStateType.MeleeAirEnemyIdle);
            _enemy.DestroyEnemy();
        }
    }
}