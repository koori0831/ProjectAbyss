using UnityEngine;

public class EnemyIdleState : State<EnemyStateType>
{
    private Enemy _enemy;
    public EnemyIdleState(Entity entity, string animaName, StateMachine<EnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as Enemy;
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
        {
            _stateMachine.ChangeState(EnemyStateType.EnemyChase);
        }
    }
}
