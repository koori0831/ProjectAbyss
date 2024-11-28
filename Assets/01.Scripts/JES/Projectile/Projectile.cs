using System;
using UnityEngine;

public abstract class Projectile : Entity
{
    protected bool _isDead = false; //총알이 이미 폭발되어 소모되었는가?
    protected float _timer = 0; //생존시간
    [SerializeField] protected float _lifeTime;

    protected Rigidbody2D _rigidBody;

    protected override void Awake()
    {
        base.Awake();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    public void ResetItem()  //풀매니징 할 때 사용할 매서드
    {
        _isDead = false;
        _timer = 0;
    }

    public virtual void InitAndFire(Transform firePosTrm)
    {
        transform.position = firePosTrm.position;
        transform.rotation = firePosTrm.rotation;
        _timer = _lifeTime;
    }
}
