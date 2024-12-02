using System;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Direct")] 
    public float ditectRange;
    [SerializeField] protected LayerMask _whatIsTarget;
    
    [Header("Combat")] 
    public float attackRange;
    public Player Target { get; protected set; }

    /// <summary>
    /// 타겟 감지하는 함수 감지하면 true, 아니면 false를 반환함. 감지하면 Target에 넣어줌
    /// </summary>
    /// <returns></returns>
    public abstract bool DitectTarget();

    
}