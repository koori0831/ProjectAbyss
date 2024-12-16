using System;
using Chipmunk.ZipLineSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private PlayerInputSO _playerInput;
    private EntityRenderer _entityRenderer;

    private float _attackDelay;

    public Weapon Weapon { get; private set; }
    public TrailRenderer TrailRenderer { get; private set; }

    public PlayerZipLineGun ziplinegun;

    public void Initialize(Player player)
    {
        _player = player;
        _entityRenderer = _player.GetCompo<EntityRenderer>();
        _playerInput = _player.GetPlayerCompo<PlayerInputSO>();

        TrailRenderer = GetComponentInChildren<TrailRenderer>();
        TrailRenderer.enabled = false;

        Weapon = GetComponentInChildren<Weapon>();
        Weapon.Intialize(_player);

        _playerInput.AttackEvent += HandleAttackEvent;

    }

    private void Update()
    {
        // ???, ?????
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Weapon.gameObject.SetActive(true);
            ziplinegun.gameObject.SetActive(false);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
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
