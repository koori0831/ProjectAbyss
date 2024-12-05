using UnityEngine;

public class StatsArtifactSO : ArtifactSO
{
    // 플레이어 스탯 구현 후 구현 
    /// <summary>
    /// 공격력 버프 수치
    /// </summary>
    [Header ("공격력")]
    [field: SerializeField] public int ATK;
    /// <summary>
    /// 체력 버프 수치
    /// </summary>
    [Header("체력")]
    [field: SerializeField] public int HP;
    /// <summary>
    /// 방어력 버프 수치
    /// </summary>
    [Header("방어력")]
    [field: SerializeField] public int DEF;
    /// <summary>
    /// 이동 속도 버프 수치
    /// </summary>
    [Header("이동 속도")]
    [field: SerializeField] public int SPD;
    /// <summary>
    /// 스테미나(기력) 버프 수치
    /// </summary>
    [Header("스테미나(기력)")]
    [field: SerializeField] public int SP;
}
