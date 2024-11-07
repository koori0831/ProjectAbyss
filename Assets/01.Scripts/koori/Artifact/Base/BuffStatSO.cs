using UnityEngine;

[CreateAssetMenu(fileName = "BuffStat", menuName = "SO/BuffStat")]
public class BuffStatSO : ScriptableObject
{
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
