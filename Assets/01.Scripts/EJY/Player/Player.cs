using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerStateEnum
{
    PlayerIdle,
    PlayerMove,
    PlayerJump,
    PlayerFall,
    PlayerZipLine,
    PlayerInteraction,
    PlayerAttack
}

public class Player : Entity
{
    [field : SerializeField]
    public PlayerInputSO InputCompo { get; private set; }
    public StateMachine<PlayerStateEnum> StateMachine { get; private set; }

    private Dictionary<Type, IPlayerComponent> _playerComponents = new Dictionary<Type, IPlayerComponent>();

    public EntityMover MoveCompo { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        GetComponentsInChildren<IPlayerComponent>(true).ToList()
            .ForEach(component => _playerComponents.Add(component.GetType(), component));

        _playerComponents.Add(InputCompo.GetType(), InputCompo);

        InitPlayerCompo();

        StateMachine = new StateMachine<PlayerStateEnum>(this);
        StateMachine.InitState(PlayerStateEnum.PlayerIdle);
    }
    private void Update()
    {
        StateMachine.StateUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.StateFixedUpdate();
    }

    protected override void AfterInit()
    {
        base.AfterInit();

        MoveCompo = GetCompo<EntityMover>();
    }

    private void InitPlayerCompo()
    {
        _playerComponents.Values.ToList().ForEach(component => component.Initialize(this));
    }

    public T GetPlayerCompo<T>(bool isDerived = false) where T : IPlayerComponent
    {
        if (_playerComponents.TryGetValue(typeof(T), out var component))
        {
            return (T)component;
        }

        if (isDerived != false)
        {
            Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];
        }

        return default;
    }
}