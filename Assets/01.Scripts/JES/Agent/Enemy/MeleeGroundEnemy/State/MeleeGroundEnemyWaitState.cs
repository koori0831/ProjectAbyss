using UnityEngine;

public class MeleeGroundEnemyWaitState : State<MeleeGroundEnemyStateType>
{
    private MeleeGroundEnemy _enemy  = null;
    private MeleeGroundEnemyAtkCompo _atkCompo = null;

    public MeleeGroundEnemyWaitState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeGroundEnemy;
        _atkCompo = _enemy.GetCompo<MeleeGroundEnemyAtkCompo>();
    }
    public override void Enter()
    {
        base.Enter();
        _entityMover.StopImmediately();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!_atkCompo.CanAttack()) return;
        
        float distance =Vector3.Distance(_entity.transform.position, _enemy.Target.transform.position);

        _stateMachine.ChangeState(_enemy.attackRange > distance
            ? MeleeGroundEnemyStateType.MeleeGroundEnemyAttack
            : MeleeGroundEnemyStateType.MeleeGroundEnemyChase);
    }
}