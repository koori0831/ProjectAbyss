using Unity.VisualScripting;
using UnityEngine;

public class RangeGroundEnemyFindState : State<RangeGroundEnemyStateType>
{
    private RangeGroundEnemy _rangeGroundEnemy;
    public RangeGroundEnemyFindState(Entity entity, string animaName, StateMachine<RangeGroundEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _rangeGroundEnemy = entity as RangeGroundEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        _entityMover.StopImmediately();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_rangeGroundEnemy.DitectTarget())
        {
            _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyWait);
            return; 
        }
        var ray = Physics2D.Raycast(new Vector3(_entity.transform.position.x,_entity.transform.position.y+0.2f,0), 
            _entity.transform.right, 0.6f,_entityMover._whatIsGround);
        if (ray.collider != null||!_entityMover.isGround.Value)
        {
            _renderer.Flip();
        }
        _entityMover.SetXMovement(_renderer.FacingDirection);
    }
    
}
