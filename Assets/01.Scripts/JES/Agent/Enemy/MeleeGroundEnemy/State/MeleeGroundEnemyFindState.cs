using UnityEngine;

public class MeleeGroundEnemyFindState : State<MeleeGroundEnemyStateType>
{
    private MeleeGroundEnemy _enemy;
    public MeleeGroundEnemyFindState(Entity entity, string animaName, StateMachine<MeleeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeGroundEnemy;
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
            _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyWait);
            return; 
        }
        var ray = Physics2D.Raycast(new Vector3(_entity.transform.position.x,_entity.transform.position.y+0.2f,0), 
            _entity.transform.right, 0.6f,_entityMover._whatIsGround);
        if (ray.collider != null||!_entityMover.isGround.Value)
        {
            _renderer.Flip();
        }
        _entityMover.SetXMovement(_renderer.FacingDirection);
        _entityMover.MoveCharacter();
    }
}