public class MeleeGroundEnemyDeathState : State<MeleeGroundEnemyStateType>
{
    private Enemy _enemy;
    
    public MeleeGroundEnemyDeathState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
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