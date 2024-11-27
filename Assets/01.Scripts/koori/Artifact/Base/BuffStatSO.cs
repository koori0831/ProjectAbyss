using UnityEngine;

[CreateAssetMenu(fileName = "BuffStat", menuName = "SO/BuffStat")]
public class BuffStatSO : ScriptableObject
{
    // 최종적으로 어떻게 플레이어가 에네미를 공격했을 때 데미지가 들어가는지에 대한 식
    // 에네미가 공격했을 때 어떻게 데미지가 계산되는지
    //공격력, 체력, 방어력, 이속, 기력
    /// <summary>
    /// 공격력
    /// </summary>
    public int ATK;
    /// <summary>
    /// 체력
    /// </summary>
    public int HP;
    /// <summary>
    /// 방어력
    /// </summary>
    public int DEF;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public int SPD;
    /// <summary>
    /// 스테미나(기력)
    /// </summary>
    public int SP;
}
