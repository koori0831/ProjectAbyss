using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : Entity,IPoolable
{
    [Header("Direct")] 
    public float ditectRange;
    [SerializeField] protected LayerMask _whatIsTarget,_whatIsObstacle;
    
    [Header("Combat")] 
    public float attackRange;
    public Player Target { get; protected set; }

    public UnityEvent OnDeadEndEvent;

    /// <summary>
    /// 타겟 감지하는 함수 감지하면 true, 아니면 false를 반환함. 감지하면 Target에 넣어줌
    /// </summary>
    /// <returns></returns>
    public abstract bool DitectTarget();

    protected override void AfterInit()
    {
        base.AfterInit();
        
        GetCompo<EntityRenderer>().OnAnimationEnd += HandleAnimationEnd;
        var health = GetCompo<EntityHealth>();
        health.OnHitEvent += HandleHit;
        health.OnDeathEvent += HandleDead;
    }

    protected virtual void HandleDead()
    {
        //다 쓸거같아서 만들어뒀음
    }

    protected virtual void HandleHit(Entity dealer)
    {
        //다 쓸거같아서 만들어뒀음
    }

    protected virtual void HandleAnimationEnd()
    {
        // 모든 에너미 다쓰니까 만들어둠
    }

    protected override void OnDestroy()
    {
        GetCompo<EntityRenderer>().OnAnimationEnd -= HandleAnimationEnd;
            
        var health = GetCompo<EntityHealth>();
        health.OnHitEvent -= HandleHit;
        health.OnDeathEvent -= HandleDead;
    }

    public void DestroyEnemy()
    {
        OnDeadEndEvent?.Invoke();
        IsDead = true;
        PoolManager.Instance.Push(this);
    }
    public string PoolName => _poolName;
    [SerializeField] private string _poolName = "Enemy";
    public GameObject ObjectPrefab => gameObject;
    public virtual void ResetItem()
    {
        IsDead = false;
        Target = null;
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