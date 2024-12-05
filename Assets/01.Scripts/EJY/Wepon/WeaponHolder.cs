using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private PlayerInputSO _playerInput;
    private EntityRenderer _entityRenderer;

    private float _attackDelay;

    public Weapon Weapon { get; private set; }

    public ZipLineGun ziplinegun;

    public void Initialize(Player player)
    {
        _player = player;
        _entityRenderer = _player.GetCompo<EntityRenderer>();
        _playerInput = _player.GetPlayerCompo<PlayerInputSO>();

        Weapon = GetComponentInChildren<Weapon>();
        Weapon.Intialize(_player);

        _playerInput.AttackEvent += HandleAttackEvent;

    }

    private void Update()
    {
        // 교체, 야매임
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Weapon.gameObject.SetActive(true);
            ziplinegun.gameObject.SetActive(false);
        }
        else if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Weapon.gameObject.SetActive(false);
            ziplinegun.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _playerInput.AttackEvent -= HandleAttackEvent;
    }

    private void HandleAttackEvent()
    {
        if (_player.canAttack == false) return;

        if (_attackDelay < Time.time)
        {
            Weapon?.Attack();
        }
    }

}
