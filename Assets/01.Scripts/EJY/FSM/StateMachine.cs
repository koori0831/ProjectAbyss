using System;
using System.Collections.Generic;
using System.Diagnostics;

public class StateMachine<T> where T : Enum
{
    private Entity _entity;

    private T _currentState;
    private Dictionary<T, State<T>> _entityState = new Dictionary<T, State<T>>();

    public StateMachine(Entity entity)
    {
        _entity = entity;
        CreateState();
    }

    public void InitState(T state)
    {
        _currentState = state;
    }

    public void StateUpdate()
    {
        _entityState[_currentState].StateUpdate();
    }

    public void StateFixedUpdate()
    {
        _entityState[_currentState].StateFixedUpdate();
    }

    public void ChangeState(T state)
    {
        _entityState[_currentState].Exit();
        _currentState = state;
        _entityState[_currentState].Enter();
    }

    private void CreateState()
    {
        foreach (T stateEnum in Enum.GetValues(typeof(T)))
        {
            string enumName = stateEnum.ToString();
            Type t = Type.GetType(enumName + "State");

            State<T> state = Activator.CreateInstance(t, _entity, enumName, this) as State<T>;

            _entityState.Add(stateEnum, state);
        }
    }
}
