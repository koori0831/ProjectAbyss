using UnityEngine;

public class MeleeGroundEnemyAtkCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    
    private MeleeGroundEnemy _enemy;
    private float _lastAtkTime;
    private EntityRenderer _animator;
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as MeleeGroundEnemy;
        Debug.Assert(_enemy != null, "Check!, Bomber attack component attached wrong!");
        
        _animator = _enemy.GetCompo<EntityRenderer>();
    }
    
    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void EnteringAttack()
    {
        _animator.OnAttackTryEvent += Attack; 
    }
    
    public void Attack()
    {
        _lastAtkTime = Time.time;
        _enemy.GetCompo<OverlapDamageCaster>().CastDamage();
        _animator.OnAttackTryEvent -= Attack;
    }
}