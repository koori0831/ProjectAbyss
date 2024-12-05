using System;
using UnityEngine;

public abstract class Shooter : MonoBehaviour, IPlayerComponent
{
    [SerializeField] protected PoolItemSO _bulletPrefab;
    [SerializeField] protected Transform _firePosTrm;

    [SerializeField] private float _availableFireTime = 0.2f;
    [SerializeField] private float _rotateLimitMinValue = -30;
    [SerializeField] private float _rotateLimitMaxValue = 50;
    [SerializeField] protected float _shootPoewr = 15;

    protected PlayerInputSO _input;
    protected EntityRenderer _renderer;

    protected Player _player;

    public event Action OnFireEvent;

    public virtual void Initialize(Player player)
    {
        _player = player;

        _input = _player.GetPlayerCompo<PlayerInputSO>();
        _renderer = _player.GetCompo<EntityRenderer>();

        _input.AttackEvent += TryShooting;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        Vector2 mouseDirection = _player.transform.InverseTransformPoint(_input.MousePos);

        float currentAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, _rotateLimitMinValue, _rotateLimitMaxValue));
    }


    public virtual void TryShooting()
    {
        if (_availableFireTime < Time.time && _player.canAttack)
        {
            if(gameObject.activeSelf)
            Attack();
        }
    }

    protected abstract void Attack();

    protected virtual void OnDestroy()
    {
        _input.AttackEvent -= TryShooting;
    }
}
