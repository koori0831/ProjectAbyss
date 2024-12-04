using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Dictionary<Type, IEntityComponent> _components;
    public bool IsDead { get; set; }

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();
        GetComponentsInChildren<IEntityComponent>(true).ToList()
            .ForEach(component => _components.Add(component.GetType(), component));

        InitComponents();
        AfterInit();
    }

    private void InitComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    protected virtual void AfterInit()
    {
        _components.Values.ToList().ForEach(component =>
        {
            if (component is IAtferInitable atferInitable)
            {
                atferInitable.AfterInit();

            }
        });
    }

    public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
    {
        if (_components.TryGetValue(typeof(T), out var compo))
        {
            return (T)compo;
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
