using System.Collections;
using UnityEngine;

public class MeleeAirEnemyAtkCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float _cooldown;
    
    private MeleeAirEnemy _enemy;
    private EntityRenderer _animator;
    
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as MeleeAirEnemy;
        Debug.Assert(_enemy != null, "Check!, Bomber attack component attached wrong!");
        
        _animator = _enemy.GetCompo<EntityRenderer>();
    }
    

    public void EnteringAttack()
    {
        _animator.OnAttackTryEvent += Attack; 
        
    }
    
    public void Attack()
    {
        _animator.AnimationSpeedSetting(0);
        
        Vector2 direction = transform.position - _enemy.Target.transform.position;

        
        StartCoroutine(AtkCor());
        
        _animator.OnAttackTryEvent -= Attack;
    }

    private IEnumerator AtkCor()
    {
        _enemy.IsAttacking = true;
        
        yield return new WaitUntil(()=>_enemy.IsAttacking == false);
    }
}
