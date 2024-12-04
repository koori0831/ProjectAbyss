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
        var ray = Physics2D.Raycast(new Vector3(_entity.transform.position.x,_entity.transform.position.y+0.2f,0), 
            _entity.transform.right, 0.6f,_entityMover._whatIsGround);
        if (ray.collider != null || !_entityMover.isGround.Value)
        {
            _enemy.Target = null;
            _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyFind);
            return;
        }

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