using System;
using UnityEngine;
using UnityEngine.Events;

public class ParabolaBullet : Projectile,IPoolable
{
    public string PoolName =>_poolName;
    [SerializeField] private string _poolName;
    public GameObject ObjectPrefab =>gameObject;


    public UnityEvent OnExplosionEvent;

    private OverlapDamageCaster _damageCaster;
    public void FireParabola(Vector2 velocity)
    {
        _damageCaster = GetCompo<OverlapDamageCaster>();
        _rigidBody.AddForce(velocity, ForceMode2D.Impulse);
    }
    
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 && !_isDead)
        {
            ExplosionEvent();
        }
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(_rigidBody.linearVelocityY,_rigidBody.linearVelocityX)*Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isDead) return;
        if (other.CompareTag("Player"))
        {
            ExplosionEvent();
        }
    }

    private void ExplosionEvent()
    {
        _damageCaster.CastDamage();
        PoolManager.Instance.Push(this);
        OnExplosionEvent?.Invoke();
        _isDead = true;
    }
}
