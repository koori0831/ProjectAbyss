using System;
using UnityEngine;

public class EntityRenderer : MonoBehaviour, IEntityComponent
{
    public event Action OnAnimationEnd;
    public event Action OnAttackTryEvent;


    public float FacingDirection { get; private set; } = 1;

    private Entity _entity;
    private Animator _animator;
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int animHash) => _animator.Play(animHash);

    public void AnimationToEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    public void AttackTry()
    {
        OnAttackTryEvent?.Invoke();
    }

    #region Flip Controller
    public void Flip()
    {
        FacingDirection *= -1;
        _entity.transform.localScale = new Vector3(_entity.transform.localScale.x * -1 ,1,1);
    }

    public void FlipController(float xMove)
    {
        if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
            Flip();
    }

    public bool isFacingRight()
    {
        return Mathf.Sign(_entity.transform.localScale.x) > 0;
    }
    #endregion
}
