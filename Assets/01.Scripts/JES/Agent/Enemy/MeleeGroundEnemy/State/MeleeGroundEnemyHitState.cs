using UnityEngine;

public class MeleeGroundEnemyHitState : State<MeleeGroundEnemyStateType>
{
    private MeleeGroundEnemy _enemy;
    public MeleeGroundEnemyHitState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeGroundEnemy;
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
            _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyFind);
    }
}