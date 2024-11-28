using System;
using UnityEngine;

public class ParabolaBullet : Projectile,IPoolable
{
    public string PoolName =>_poolName;
    [SerializeField] private string _poolName;
    public GameObject ObjectPrefab =>gameObject;
    
    
    public void FireParabola(Vector2 velocity)
    {
        _rigidBody.linearVelocity = velocity;
    }
    public override void InitAndFire(Transform firePosTrm)
    {
        transform.position = firePosTrm.position;
        transform.rotation = firePosTrm.rotation;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && !_isDead)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
