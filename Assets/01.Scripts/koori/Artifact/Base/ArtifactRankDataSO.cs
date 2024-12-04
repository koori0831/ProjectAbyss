using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactRankData", menuName = "SO/ArtifactRank")]
public class ArtifactRankDataSO : ScriptableObject
{
    public ArtifactRank ArtifactRank;
    public string RankName;
    public Color RankColor = Color.white;
    [Range(1, 100)]
    public int RankRarity = 1;
}
