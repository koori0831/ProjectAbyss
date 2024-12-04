using UnityEngine;

public class ArtifactRankSO : ScriptableObject
{
    public string RankName;
    public Color RankColor = Color.white;
    [Range(1, 100)]
    public int RankRarity = 1;
}
