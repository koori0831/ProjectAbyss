using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponArtifactSO _startWeaponData;

    private Player _player;
    private SpriteRenderer _weaponSprite;

    public NotifyValue<WeaponArtifactSO> currentWeaponData;
    public float attackDeleay;

    public WeaponType WeaponType { get; private set; }

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

    }
}
