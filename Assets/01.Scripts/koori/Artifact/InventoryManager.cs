using System.Collections.Generic;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public List<WeaponArtifactSO> HaveWeaponList { get; private set; }
    public List<SkillArtifactSO> HaveSkillArtifactList { get; private set; }
    public List<StatsArtifactSO> HaveStatArtifactList { get; private set; }
    public int HaveMagicStoneCount { get; private set; }

    public void GetWeapon(WeaponArtifactSO item) { HaveWeaponList.Add(item); }
    public void GetSkillArtifact(SkillArtifactSO item) { HaveSkillArtifactList.Add(item); }
    public void GetStatArtifact(StatsArtifactSO item) { HaveStatArtifactList.Add(item); }
}
