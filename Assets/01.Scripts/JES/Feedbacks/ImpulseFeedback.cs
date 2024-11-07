using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CinemachineImpulseSource))]
public class ImpulseFeedback : Feedback
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float _impulsePower = 0.3f;
    private CinemachineImpulseSource _source;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public override void PlayFeedback()
    {
        if(_gun != null)
            _source.GenerateImpulse(_gun.gunData.impulsePower);
        else
            _source.GenerateImpulse(_impulsePower);
    }

    public override void StopFeedback()
    {
        
    }
}
