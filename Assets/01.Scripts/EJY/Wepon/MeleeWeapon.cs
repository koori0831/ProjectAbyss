using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IPlayerComponent
{
    private Animator _animator;
    private Player _player;
    private PlayerInputSO _playerInputSO;

    public float _availableFireTime = 0.2f;

    private readonly int _atkHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void TryShooting()
    {
        if (_availableFireTime < Time.time && _player.canFire)
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerAttack);
        }
    }

    public void AtkAnimation()
    {
        _animator.SetTrigger(_atkHash);   
    }

    public void Initialize(Player player)
    {
        _player = player;
        _playerInputSO = _player.GetPlayerCompo<PlayerInputSO>();

        _playerInputSO.AttackEvent += TryShooting;
        _player.OnAttackEvent += AtkAnimation;
    }

    private void OnDestroy()
    {
        _playerInputSO.AttackEvent -= TryShooting;
        _player.OnAttackEvent -= AtkAnimation;
    }
}
