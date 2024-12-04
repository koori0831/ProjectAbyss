public class MeleeAirEnemyIdleState : State<MeleeAirEnemyStateType>
{
    private MeleeAirEnemy _enemy;
    public MeleeAirEnemyIdleState(Entity entity, string animaName, StateMachine<MeleeAirEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeAirEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        _entityMover.StopImmediately();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_enemy.DitectTarget())
            _stateMachine.ChangeState(MeleeAirEnemyStateType.MeleeAirEnemyAttack);
    }
}