using System.Collections;
using UnityEngine;

public class MeleeAirEnemyAtkCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _dashPower=30f;
    
    private MeleeAirEnemy _enemy;
    private EntityRenderer _animator;
    private EntityMover _mover;
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as MeleeAirEnemy;
        Debug.Assert(_enemy != null, "Check!, Bomber attack component attached wrong!");
        
        _animator = _enemy.GetCompo<EntityRenderer>();
        _mover = _enemy.GetCompo<EntityMover>();
    }
    

    public void EnteringAttack()
    {
        _animator.OnAttackTryEvent += Attack; 
    }
    
    public void Attack()
    {
        _animator.AnimationSpeedSetting(0);
        StartCoroutine(AtkCor());
        
        _animator.OnAttackTryEvent -= Attack;
    }

    private IEnumerator AtkCor()
    {
        _enemy.IsAttacking = true;
        yield return new WaitForSeconds(0.5f);
        
        Vector2 direction = _enemy.Target.transform.position -_enemy.transform.position;
        _mover.AddForceToEntity(direction.normalized*_dashPower);
        
        yield return new WaitUntil(()=>_enemy.IsAttacking == false);
        _mover.StopImmediately(true);
        _animator.AnimationSpeedSetting(1);
    }
}
