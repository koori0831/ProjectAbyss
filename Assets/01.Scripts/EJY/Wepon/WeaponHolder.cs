using UnityEngine;

public class WeaponHolder : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private SpriteRenderer _weaponSprite;
    private NotifyValue<Weapon> _weapon;

    public void Initialize(Player player)
    {
        _player = player;
        _weapon = new NotifyValue<Weapon>();
        _weaponSprite = GetComponent<SpriteRenderer>();
    }

}
