using UnityEngine;

public class SkillArtifactSO : ArtifactSO, ISkillable
{
    [field: SerializeField] public SkillSO SkillSO { get; private set; }
}
