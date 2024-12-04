using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, IPlayerActions, IPlayerComponent
{
    public event Action JumpEvent;
    public event Action AttackEvent;
    public event Action InteractionEvent;
    public event Action ZipShootEvent;

    public Vector2 InputDirection { get; private set; }

    private Controls _controls;

    private Player _player;

    private Vector2 _mousePos;
    public Vector2 MousePos
    {
        get
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(_mousePos);
            worldPos.z = 0;
            return worldPos;
        }
    }

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
        if(context.performed)
            InteractionEvent?.Invoke();
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

    public void OnMouse(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }
}