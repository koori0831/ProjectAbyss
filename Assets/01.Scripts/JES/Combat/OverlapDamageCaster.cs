using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    [SerializeField] private Vector2 _castSize;
    private Collider2D[] _colliders;

    public override void InitCaster(Entity owner)
    {
        base.InitCaster(owner);
        _colliders = new Collider2D[_maxAvailableCount];
    }

    public override void CastDamage()
    {
        Vector2 start = (Vector2)transform.position - _castSize * 0.5f;
        Vector2 end = start + _castSize;

        int cnt = Physics2D.OverlapArea(start, end, _contactFilter, _colliders);

        Vector2 atkDirection = _owner.transform.right;
        Vector2 knockbackForce = _knockbackForce;
        knockbackForce.x *= atkDirection.x; //공격방향으로 넉백 설정하고

        for (int i = 0; i < cnt; i++)
        {
            if (_colliders[i].TryGetComponent(out IDamageable damageable))
            {
                //아직 인터페이스를 안만들어셔 여기까지만.
                damageable.ApplyDamage(_damage, atkDirection, knockbackForce, _owner);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _castSize);
    }
}
#endif