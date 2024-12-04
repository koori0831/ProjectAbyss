using UnityEngine;

public class MeleeGroundEnemyChaseState : State<MeleeGroundEnemyStateType>
{
    private MeleeGroundEnemy _enemy;
    public MeleeGroundEnemyChaseState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeGroundEnemy;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        
        float distance =Vector3.Distance(_entity.transform.position, _enemy.Target.transform.position);
        if (_enemy.attackRange > distance)
        {
            _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyWait);
            return;
        }
        
        Vector2 direction = _enemy.Target.transform.position - _entity.transform.position;
        
        _entityMover.SetXMovement(direction.normalized.x);
        _entityMover.MoveCharacter();
    }
}