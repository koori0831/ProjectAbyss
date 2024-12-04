using UnityEngine;

public class MeleeGroundEnemy : Enemy
{
    private StateMachine<MeleeGroundEnemyStateType> _stateMachine;

    protected override void AfterInit()
    {
        base.AfterInit();
        _stateMachine = new StateMachine<MeleeGroundEnemyStateType>(this);
        _stateMachine.InitState(MeleeGroundEnemyStateType.MeleeGroundEnemyFind);
    }

    protected override void HandleDead()
    {
        _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyDeath);
    }

    protected override void HandleHit(Entity dealer)
    {
        if (IsDead) return;
        Target = dealer as Player;
        _stateMachine.ChangeState(MeleeGroundEnemyStateType.MeleeGroundEnemyHit);
    }

    protected override void HandleAnimationEnd()
    {
        _stateMachine.CurrentState().AnimationEndTrigger();
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
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y+0.2f), Vector2.right, ditectRange,_whatIsTarget);

        if (hit.collider != null && hit.collider.TryGetComponent(out Player player))
        {
            Target = player;
            return true;
        }
        return false;
    }

    protected override void OnDrawGizmos()
    {
        Vector3 start = new Vector2(transform.position.x,transform.position.y+0.2f);
        Vector3 end = start + transform.right * ditectRange;

        // 기즈모 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(start, 0.1f); // 시작점 표시
        Gizmos.DrawSphere(end, 0.1f); // 끝점 표시
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.white;
    }
}

public enum MeleeGroundEnemyStateType
{
    MeleeGroundEnemyFind,
    MeleeGroundEnemyChase,
    MeleeGroundEnemyWait,
    MeleeGroundEnemyAttack,
    MeleeGroundEnemyDeath,
    MeleeGroundEnemyHit
}
