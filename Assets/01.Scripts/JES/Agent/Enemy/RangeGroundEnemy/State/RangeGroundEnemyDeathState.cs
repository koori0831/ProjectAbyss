public class RangeGroundEnemyDeathState : State<RangeGroundEnemyStateType>
{
    private Enemy _enemy;
    public RangeGroundEnemyDeathState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
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