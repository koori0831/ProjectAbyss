using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    PlayerIdle,
    PlayerMove,
    PlayerZipLine,
    PlayerInteraction,
    PlayerAttack
}

public class Player : Entity
{
    [field : SerializeField]
    public PlayerInputSO InputCompo { get; private set; }
    public StateMachine<PlayerState> StateMachine { get; private set; }

    private Dictionary<Type, IPlayerComponent> _playerComponents = new Dictionary<Type, IPlayerComponent>();

    protected override void Awake()
    {
    }

    private void InitPlayerCompo()
    {
    }

    private void Update()
    {
        StateMachine.StateUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.StateFixedUpdate();
    }
}