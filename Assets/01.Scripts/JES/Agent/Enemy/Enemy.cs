using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Entity
{
    protected StateMachine<EnemyStateType> stateMachine;

    [Header("Direct")] 
    public float ditectRange;
    [SerializeField] private LayerMask _whatIsTarget;

    [Header("Combat")] 
    public int damage;
    public float knockPower;
    public float attackRange;
    public Player Target { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine.Initialize(this);
    }

    private void Update()
    {
        stateMachine.StateUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.StateFixedUpdate();
    }

    /// <summary>
    /// 타겟 감지하는 함수 감지하면 true, 아니면 false를 반환함. 감지하면 Target에 넣어줌
    /// </summary>
    /// <returns></returns>
    public bool DitectTarget()
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


public enum EnemyStateType
{
    EnemyIdle,EnemyChase,EnemyHit,EnemyDeath,EnemyAttack
}