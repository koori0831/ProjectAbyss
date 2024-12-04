using UnityEngine;

public class SkillWeaponArtifactSO : WeaponArtifactSO, ISkillable
{
    [field: SerializeField] public SkillSO SkillSO { get; }
}