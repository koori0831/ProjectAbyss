using UnityEngine;

public class RangeGroundEnemyAttackState : State<RangeGroundEnemyStateType>
{
    private RangeGroundEnemy _enemy;
    private RangeEnemyAtkCompo _atkCompo;
    public RangeGroundEnemyAttackState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as RangeGroundEnemy;
        _atkCompo = _enemy.GetCompo<RangeEnemyAtkCompo>();
    }
    

    public override void Enter()
    {
        base.Enter();
        FacingToPlayer();

        if (_atkCompo.CanAttack())
            _atkCompo.EnteringAttack();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_isTriggerCall)
        {
            _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyWait);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _enemy.Target.transform.position.x - _entity.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}