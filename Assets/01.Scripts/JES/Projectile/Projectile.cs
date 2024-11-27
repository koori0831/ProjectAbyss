using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    protected bool _isDead = false; //총알이 이미 폭발되어 소모되었는가?
    protected float _timer = 0; //생존시간

    protected Rigidbody2D _rigidBody;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ResetItem()  //풀매니징 할 때 사용할 매서드
    {
        _isDead = false;
        _timer = 0;
    }

    public abstract void InitAndFire(Transform firePosTrm, int damage, float knockBackPower);

}
