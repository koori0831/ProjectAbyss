using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        /// clamp 거니까 그런 듯 어카지? 총의 회전은 무조건적으로 필요하다고 봄
        /// 전방 외에 입력을 아예 무시/

        
        Vector2 mouseDirection = _player.transform.InverseTransformPoint(_input.MousePos);

        float currentAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, _rotateLimitMinValue, _rotateLimitMaxValue));
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
        _input.AttackEvent -= TryShooting;
    }
}
