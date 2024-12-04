using System;
using UnityEngine;

public abstract class State<T> where T : Enum
{
    protected Entity _entity;
    protected StateMachine<T> _stateMachine;
    protected int _animaHash;
    protected bool _isTriggerCall;
    
    #region EntityComponent
    protected EntityRenderer _renderer;
    protected EntityMover _entityMover;
    #endregion

    public State(Entity entity, string animaName, StateMachine<T> stateMachine)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animaHash = Animator.StringToHash(animaName);

        _renderer = _entity.GetCompo<EntityRenderer>();
        _entityMover = _entity.GetCompo<EntityMover>();
    }

    public virtual void Enter()
    {
        _renderer.PlayAnimation(_animaHash);
    }

    public virtual void StateUpdate()
    {
    }

    public virtual void StateFixedUpdate()
    {
    }

    public virtual void Exit()
    {
        _isTriggerCall = false;
    }

    public virtual void AnimationEndTrigger()
    {
        _isTriggerCall = true;
    }
}
