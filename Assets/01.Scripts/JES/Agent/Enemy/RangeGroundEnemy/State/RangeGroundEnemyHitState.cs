using UnityEngine;

public class RangeGroundEnemyHitState : State<RangeGroundEnemyStateType>
{
    private RangeGroundEnemy _enemy;
    public RangeGroundEnemyHitState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as RangeGroundEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        Vector3 dir = _enemy.Target.transform.position - _enemy.transform.position;
        
        _renderer.FlipController(dir.x);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if(_isTriggerCall)
            _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyFind);
    }
}
