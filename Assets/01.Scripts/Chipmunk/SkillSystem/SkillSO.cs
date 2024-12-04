using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    [Tooltip("스킬 선딜레이")]
    [SerializeField] public float skillCool;
    public abstract void OnUseSkill(Player player);
}
