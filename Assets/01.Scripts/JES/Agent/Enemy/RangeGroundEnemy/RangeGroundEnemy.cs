using UnityEngine;

public class RangeGroundEnemy : Enemy
{
    private StateMachine<RangeGroundEnemyStateType> _stateMachine;
    

    protected override void AfterInit()
    {
        base.AfterInit();
        
        _stateMachine = new StateMachine<RangeGroundEnemyStateType>(this);
        _stateMachine.InitState(RangeGroundEnemyStateType.RangeGroundEnemyFind);
        
        
    }


    protected override void HandleDead()
    {
        _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyDeath);
    }

    protected override void HandleHit(Entity dealer)
    {
        if (IsDead) return;
        Target = dealer as Player;
        _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyHit);
    }

    protected override void HandleAnimationEnd()
    {
        _stateMachine.CurrentState().AnimationEndTrigger();
    }

    private void OnDestroy()
    {
        
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }
    private void FixedUpdate()
    {
        _stateMachine.StateFixedUpdate();
    }
    
    public override bool DitectTarget()
    {
        var target = Physics2D.OverlapCircle(transform.position, ditectRange, _whatIsTarget);
        if (target != null&&target.TryGetComponent(out Player player))
        {
            Target = player;
            return true;
        }
        return false;
    }
}


public enum RangeGroundEnemyStateType
{
    RangeGroundEnemyFind,RangeGroundEnemyWait,RangeGroundEnemyHit,RangeGroundEnemyDeath,RangeGroundEnemyAttack
}