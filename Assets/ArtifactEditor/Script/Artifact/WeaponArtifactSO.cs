using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}
public class WeaponArtifactSO : ArtifactSO
{
    [Header ("Weapon Info")]
    [field: SerializeField] public WeaponType WeaponType = WeaponType.Melee;
    [field: SerializeField] public int WeaponAtk = 5;
    [field: SerializeField] public float AtkRange = 5;
    [field: SerializeField] public float AtkSpeed = 5;
    [field: SerializeField] public int KnockBackPower = 5;
    [field: SerializeField] public SkillSO SkillSO { get; private set; }
}
