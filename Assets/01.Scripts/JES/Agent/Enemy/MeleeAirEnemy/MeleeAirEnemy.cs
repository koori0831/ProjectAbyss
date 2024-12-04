using System;
using UnityEngine;

public class MeleeAirEnemy : Enemy
{
    public bool IsAttacking { get; set; } = false;

    private StateMachine<MeleeAirEnemyStateType> _stateMachine;
    

    protected override void AfterInit()
    {
        base.AfterInit();

        _stateMachine = new StateMachine<MeleeAirEnemyStateType>(this);
        _stateMachine.InitState(MeleeAirEnemyStateType.MeleeAirEnemyIdle);
    }

    protected override void HandleDead()
    {
        _stateMachine.ChangeState(MeleeAirEnemyStateType.MeleeAirEnemyDeath);
    }
    protected override void HandleHit(Entity dealer)
    {
        if (IsDead) return;
        Target = dealer as Player;
        _stateMachine.ChangeState(MeleeAirEnemyStateType.MeleeAirEnemyDeath);
    }
    protected override void HandleAnimationEnd()
    {
        _stateMachine.CurrentState().AnimationEndTrigger();
    }

    public override bool DitectTarget()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, ditectRange, _whatIsTarget);

        if(col != null)
        {
            Vector2 direction = col.transform.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, _whatIsObstacle);

            if(hit.collider == null && col.TryGetComponent(out Player player))
            {
                Target = player;
                return true;
            }
        }
        return false;
    }
    
    private void Update()
    {
        _stateMachine.StateUpdate();
    }
    private void FixedUpdate()
    {
        _stateMachine.StateFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(IsAttacking==false) return;

        IsAttacking = false;
    }
}

public enum MeleeAirEnemyStateType
{
    MeleeAirEnemyIdle,MeleeAirEnemyDeath,MeleeAirEnemyAttack
}