using UnityEngine;

public abstract class DamageCaster : MonoBehaviour,IEntityComponent
{
    [SerializeField] protected ContactFilter2D _contactFilter;
    [SerializeField] protected int _maxAvailableCount = 4;
    [SerializeField] protected float _damage = 5f;
    [SerializeField] protected Vector2 _knockbackForce;

    protected Entity _owner;


    public abstract void CastDamage();

    public virtual void Initialize(Entity entity)
    {
        _owner = entity;
    }
}
