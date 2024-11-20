using System;
using UnityEngine;

public class Enemy : Entity
{
    protected StateMachine<EnemyStateType> stateMachine;

    [Header("Direct")] 
    public float ditectRange;
    [SerializeField] private LayerMask _whatIsTaget;

    [Header("Combat")] 
    public int damage;
    public float knockPower;
    public float attackRange;
    public Player Target { get; private set; }

    public bool DitectTarget()
    {
        return true;
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
    Idle,Run
}