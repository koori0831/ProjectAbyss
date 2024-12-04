using UnityEngine;

[CreateAssetMenu(fileName = "CreateGameobjectSkill", menuName = "Skill/CreateGameobjectSkill")]
public class CreateGameobjectSkill : SkillSO
{
    public GameObject prefab;
    public override void OnUseSkill(Player player)
    {
        GameObject go = Instantiate(prefab, player.transform.position, Quaternion.identity);
        
    }
}
