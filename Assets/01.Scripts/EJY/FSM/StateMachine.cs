using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : IEntityComponent where T : Enum
{
    private Entity _entity;

    private T _currentState;
    private Dictionary<T, State<T>> _entityState;

    private void Update()
    {
        _entityState[_currentState].StateUpdate();
    }

    private void FixedUpdate()
    {
        _entityState[_currentState].StateFixedUpdate();
    }

    public void ChageState(T state)
    {
        _entityState[_currentState].Exit();
        _currentState = state;
        _entityState[_currentState].Enter();
    }

    private void CreateState()
    {
        foreach (T state in Enum.GetValues(typeof(T)))
        {
            string enumName = state.ToString();
            Type t = Type.GetType(enumName + "State");

            State<T> playerState = Activator.CreateInstance(t, _entity, enumName, this) as State<T>;

            _entityState.Add(state, playerState);
        }
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _entityState = new Dictionary<T, State<T>>();
        CreateState();
    }
}
