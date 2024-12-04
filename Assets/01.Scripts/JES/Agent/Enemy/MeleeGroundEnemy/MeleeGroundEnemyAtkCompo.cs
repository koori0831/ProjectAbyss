using UnityEngine;

public class MeleeGroundEnemyAtkCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    
    private MeleeGroundEnemy _enemy;
    private float _lastAtkTime;
    private EntityRenderer _animator;
    private EntityMover _mover;
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as MeleeGroundEnemy;
        Debug.Assert(_enemy != null, "Check!, Bomber attack component attached wrong!");
        
        _animator = _enemy.GetCompo<EntityRenderer>();
        _mover = _enemy.GetCompo<EntityMover>();
    }
    
    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void EnteringAttack()
    {
        _animator.OnAttackTryEvent += Attack; 
    }
    
    public void Attack()
    {
        _mover.StopImmediately();
        _lastAtkTime = Time.time;
        Vector2 direction = _enemy.Target.transform.position - _enemy.transform.position;
        
        _mover.AddForceToEntity(direction.normalized*4);
        _enemy.GetCompo<OverlapDamageCaster>().CastDamage();
        
        
        _animator.OnAttackTryEvent -= Attack;
    }
}