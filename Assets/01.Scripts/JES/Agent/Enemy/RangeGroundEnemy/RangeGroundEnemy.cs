using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGroundEnemy : Enemy
{
    private StateMachine<RangeGroundEnemyStateType> _stateMachine;
    
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine<RangeGroundEnemyStateType>(this);
        _stateMachine.InitState(RangeGroundEnemyStateType.RangeGroundEnemyFind);
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