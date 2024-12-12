using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitFeedbackPlayer : MonoBehaviour,IEntityComponent
{
    private List<Feedback> _feedbackToPlay;

    private Entity _entity;
    private EntityHealth _entityHealth;
    public void Initialize(Entity entity)
    {
        _feedbackToPlay = GetComponents<Feedback>().ToList();

        _entity = entity;
        _entityHealth = entity.GetCompo<EntityHealth>();
        _entityHealth.OnHitEvent += PlayFeedbacks;
    }

    private void OnDestroy()
    {
        _entityHealth.OnHitEvent -= PlayFeedbacks;
    }

    public void PlayFeedbacks(Entity entity)
    {
        StopFeedbacks();
        _feedbackToPlay.ForEach(f => f.PlayFeedback());
    }
    public void StopFeedbacks()
    {
        _feedbackToPlay.ForEach(f => f.StopFeedback());
    }
}
