using UnityEngine;
public enum WeaponType
{
    Ranged,
    Melee
}

[CreateAssetMenu(fileName = "WeaponStat", menuName = "SO/WeaponStat")]
public class WeaponStatSO : ScriptableObject
{
    public WeaponType WeaponType;
    public float Damage;
    public float AttackRange;
    public float AttackSpeed;
    public float KnockBackPower;
}
