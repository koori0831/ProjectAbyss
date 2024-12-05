using UnityEngine;

public class RangeEnemyAtkCompo : MonoBehaviour,IEntityComponent
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _fireAngle = 45f;
    [SerializeField] private string _bulletName = "ParabolaBullet";
    
    private RangeGroundEnemy _enemy;
    private float _lastAtkTime;
    private EntityRenderer _animator;
    
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as RangeGroundEnemy;
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
        
        float angle = _fireAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float tan = Mathf.Tan(angle);
        float sin = Mathf.Sin(angle);
        float gravity = Physics2D.gravity.magnitude; //중력값을 가져온다.
        Vector2 direction = transform.position - _enemy.Target.transform.position;

        float distance = Mathf.Abs(direction.x);
        float yOffset = direction.y; //목적지 y값을 0이라고 계산하니까.
        
        float vZero = (1 / cos) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) 
                                             / (distance * tan + yOffset * tan));
        if (float.IsNaN(vZero))
        {
            vZero = 4f;
        }

        float xDirection = -Mathf.Sign(direction.x); //x축의 부호만 가져온다.
        
        Vector2 velocity = new Vector2(xDirection * vZero * cos, vZero * sin);

        ParabolaBullet bullet = PoolManager.Instance.Pop(_bulletName) as ParabolaBullet;
        bullet.InitAndFire(transform);
        bullet.FireParabola(velocity);
        _animator.OnAttackTryEvent -= Attack;
    }
}
