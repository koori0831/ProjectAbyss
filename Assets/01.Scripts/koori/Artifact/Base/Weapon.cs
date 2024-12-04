using UnityEngine;
public class Weapon : ArtifactData
{
    public WeaponStatSO stat;
    public Skill skill;

    public virtual  void Attack()
    {
        Debug.Log("À×, ¾ÆÀÕ ºè. º£ÀÌ½º °ø°Ý ½ÇÇà!");
    }
}
