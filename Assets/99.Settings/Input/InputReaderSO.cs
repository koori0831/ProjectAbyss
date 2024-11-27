using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, IPlayerActions, IPlayerComponent
{
    public event Action JumpEvent;
    public event Action AttackEvent;
    public event Action ZipShootEvent;

    public Vector2 InputDirection { get; private set; }

    private Controls _controls;

    private Player _player;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        InputDirection = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void OnZipLineShooter(InputAction.CallbackContext context)
    {
        if(context.performed)
            ZipShootEvent?.Invoke();
    }
}