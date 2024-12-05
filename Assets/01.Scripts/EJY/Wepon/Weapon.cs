using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponArtifactSO _startWeaponData;

    private Player _player;
    private SpriteRenderer _weaponSprite;

    public NotifyValue<WeaponArtifactSO> currentWeaponData;
    public float attackDeleay;

    public WeaponType WeaponType { get; private set; }
    [field: Header("Attack Info")]

    [field: SerializeField]
    public bool CanAttack { get; private set; } = false;

    [SerializeField] private Transform _attackCheckTrm;
    [SerializeField] private Vector2 _attackCheckSize;
    [SerializeField] private LayerMask _whatIsEnemy;

    public void Intialize(Player player)
    {
        _player = player;
        _weaponSprite = GetComponent<SpriteRenderer>();
        currentWeaponData = new NotifyValue<WeaponArtifactSO>();

        currentWeaponData.OnvalueChanged += HandleCurrentGunChange;
        currentWeaponData.Value = _startWeaponData;
    }

    private void HandleCurrentGunChange(WeaponArtifactSO prev, WeaponArtifactSO next)
    {
        if (prev != null)
        {

        }

        if (next != null)
        {

            _weaponSprite.sprite = next.ItemImage;
            attackDeleay = next.AtkSpeed;
            WeaponType = next.WeaponType;
        }
    }

    public virtual void Attack()
    {
        Debug.Log("공격");
        Collider2D attackEntity = Physics2D.OverlapBox(_attackCheckTrm.position, _attackCheckSize, 0, _whatIsEnemy);

        CanAttack = attackEntity;

        if (attackEntity == null) return;

        if (attackEntity.TryGetComponent(out IDamageable target))
        {
            Debug.Log("데미지 전달 시도");
            Vector2 knockBack = new Vector2(transform.right.x * currentWeaponData.Value.KnockBackPower, 0);
            target.ApplyDamage(currentWeaponData.Value.WeaponAtk,transform.right,knockBack,_player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_attackCheckTrm.position, _attackCheckSize);
    }
}
