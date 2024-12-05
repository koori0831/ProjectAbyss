using UnityEngine;

public class MeleeGroundEnemyAttackState : State<MeleeGroundEnemyStateType>
{
    private MeleeGroundEnemy _enemy;
    private MeleeGroundEnemyAtkCompo _atkCompo;
    public MeleeGroundEnemyAttackState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeGroundEnemy;
        _atkCompo = _enemy.GetCompo<MeleeGroundEnemyAtkCompo>();
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
            _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyFind);
        }
    }

    private void FacingToPlayer()
    {
        float xDirection = _enemy.Target.transform.position.x - _entity.transform.position.x;
        _renderer.FlipController(Mathf.Sign(xDirection));
    }
}