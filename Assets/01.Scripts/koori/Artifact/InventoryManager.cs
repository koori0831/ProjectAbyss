using System.Collections.Generic;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public List<WeaponArtifactSO> HaveWeaponList { get; private set; }
    public List<SkillArtifactSO> HaveSkillArtifactList { get; private set; }
    public List<StatsArtifactSO> HaveStatArtifactList { get; private set; }
    public int HaveMagicStoneCount { get; private set; }

    public void AddWeapon(WeaponArtifactSO item) { HaveWeaponList.Add(item); }
    public void AddSkillArtifact(SkillArtifactSO item) { HaveSkillArtifactList.Add(item); }
    public void AddStatArtifact(StatsArtifactSO item) { HaveStatArtifactList.Add(item); }
}
