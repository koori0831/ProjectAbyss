using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : Entity,IPoolable
{
    [Header("Direct")] 
    public float ditectRange;
    [SerializeField] protected LayerMask _whatIsTarget;
    
    [Header("Combat")] 
    public float attackRange;
    public Player Target { get; protected set; }

    public UnityEvent OnDeadEndEvent;

    /// <summary>
    /// 타겟 감지하는 함수 감지하면 true, 아니면 false를 반환함. 감지하면 Target에 넣어줌
    /// </summary>
    /// <returns></returns>
    public abstract bool DitectTarget();

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
}