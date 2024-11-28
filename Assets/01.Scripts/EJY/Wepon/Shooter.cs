using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Shooter : MonoBehaviour, IPlayerComponent
{
    [SerializeField] protected PoolItemSO _bulletPrefab;
    [SerializeField] protected Transform _firePosTrm;

    [SerializeField] private float _availableFireTime = 0.2f;

    [SerializeField] protected float _shootPoewr = 15;

    protected PlayerInputSO _input;
    protected EntityRenderer _renderer;

    protected Player _player;

    protected Vector2 _mousePos;

    public event Action OnFireEvent;

    public virtual void Initialize(Player player)
    {
        _player = player;

        _input = _player.GetPlayerCompo<PlayerInputSO>();
        _renderer = _player.GetCompo<EntityRenderer>();

        _input.AttackEvent += HandleGunFlipShootEvent;
        _input.AttackEvent += TryShooting;
    }

    private void Update()
    {
        RotateGun();
    }

    private void HandleGunFlipShootEvent()
    {
        Vector2 mousePos = _player.transform.InverseTransformPoint(_mousePos);
        _renderer.FlipController(MathF.Sign(mousePos.x * _renderer.FacingDirection));
    }

    private void RotateGun()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 mouseDirection = _player.transform.InverseTransformPoint(_mousePos);

        float currentAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, -30, 50));
    }


    protected virtual void TryShooting()
    {
        if (_availableFireTime < Time.time)
        {
            FireBullet();
        }
    }

    protected abstract void FireBullet();

    protected virtual void OnDestroy()
    {
        _input.AttackEvent -= HandleGunFlipShootEvent;
        _input.AttackEvent -= TryShooting;
    }
}
