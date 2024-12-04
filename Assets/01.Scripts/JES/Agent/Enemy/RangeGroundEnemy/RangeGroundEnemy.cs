using UnityEngine;

public class RangeGroundEnemy : Enemy
{
    private StateMachine<RangeGroundEnemyStateType> _stateMachine;
    

    protected override void AfterInit()
    {
        base.AfterInit();
        
        _stateMachine = new StateMachine<RangeGroundEnemyStateType>(this);
        _stateMachine.InitState(RangeGroundEnemyStateType.RangeGroundEnemyFind);
        
        GetCompo<EntityRenderer>().OnAnimationEnd += HandleAnimationEnd;
        var health = GetCompo<EntityHealth>();
        health.OnHitEvent += HandleHit;
        health.OnDeathEvent += HandleDead;
    }


    private void HandleDead()
    {
        _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyDeath);
    }

    private void HandleHit(Entity dealer)
    {
        if (IsDead) return;
        Target = dealer as Player;
        _stateMachine.ChangeState(RangeGroundEnemyStateType.RangeGroundEnemyHit);
    }

    private void HandleAnimationEnd()
    {
        _stateMachine.CurrentState().AnimationEndTrigger();
    }

    private void OnDestroy()
    {
        GetCompo<EntityRenderer>().OnAnimationEnd -= HandleAnimationEnd;
            
        var health = GetCompo<EntityHealth>();
        health.OnHitEvent -= HandleHit;
        health.OnDeathEvent -= HandleDead;
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
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ditectRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.white;
    }
    #endif
}


public enum RangeGroundEnemyStateType
{
    RangeGroundEnemyFind,RangeGroundEnemyWait,RangeGroundEnemyHit,RangeGroundEnemyDeath,RangeGroundEnemyAttack
}