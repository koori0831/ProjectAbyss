using UnityEngine;

public class MeleeAirEnemyAttackState : State<MeleeAirEnemyStateType>
{
    private MeleeAirEnemy _enemy;
    private MeleeAirEnemyAtkCompo _atkCompo;
    
    public MeleeAirEnemyAttackState(Entity entity, string animaName, StateMachine<MeleeAirEnemyStateType> stateMachine) : base(entity, animaName, stateMachine)
    {
        _enemy = entity as MeleeAirEnemy;
        _atkCompo = _enemy.GetCompo<MeleeAirEnemyAtkCompo>();
    }
    
    public override void Enter()
    {
        base.Enter();
        _atkCompo.EnteringAttack();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_isTriggerCall)
        {
            _enemy.GetCompo<OverlapDamageCaster>().CastDamage();
            _stateMachine.ChangeState(MeleeAirEnemyStateType.MeleeAirEnemyDeath);
        }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        FacingAngle();
    }

    private void FacingAngle()
    {
        float angle = Mathf.Atan2(_entityMover.Velocity.y,_entityMover.Velocity.x)*Mathf.Rad2Deg;
        _enemy.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}