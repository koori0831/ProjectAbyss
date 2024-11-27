using UnityEngine;

public class RangeGroundEnemyWaitState : State<RangeGroundEnemyStateType>
{
    private RangeGroundEnemy _enemy  = null;
    private RangeEnemyAtkCompo _atkCompo = null;
    public RangeGroundEnemyWaitState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as RangeGroundEnemy;
        _atkCompo = _enemy.GetCompo<RangeEnemyAtkCompo>();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!_atkCompo.CanAttack()) return;
        
        float distance =Vector3.Distance(_entity.transform.position, _enemy.Target.transform.position);

        if (_enemy.attackRange < distance)
        {
            _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyAttack);
        }
    }
}
