using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZipLineShooter : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Transform _firePos;

    private Player _player;
    private PlayerInputSO _input;
    private EntityRenderer _renderer;

    private Vector2 _startPos = Vector2.zero, _endPos = Vector2.zero;

    private Vector2 _mousePos;

    public void Initialize(Player player)
    {
        _player = player;
        _input = _player.GetPlayerCompo<PlayerInputSO>();
        _renderer = _player.GetCompo<EntityRenderer>();

        _input.ZipShootEvent += HandleZipLineShootEvent;
    }
    private void HandleZipLineShootEvent()
    {
        Vector2 mousePos = _player.transform.InverseTransformPoint(_mousePos);
        Debug.Log($"mouse X : {mousePos.x}, facing dir : {_renderer.FacingDirection} ");
        _renderer.FlipController(MathF.Sign(mousePos.x * _renderer.FacingDirection));
    }

    private void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 mouseDirection = _player.transform.InverseTransformPoint(_mousePos);

        float currentAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnDestroy()
    {
        _input.ZipShootEvent -= HandleZipLineShootEvent;
    }
}
