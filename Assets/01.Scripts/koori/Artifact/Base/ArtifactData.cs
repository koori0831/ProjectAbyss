using UnityEngine;

public enum ArtifactRank
{
    None,
    Special,
    High,
    Normal,
    Low
}

public abstract class ArtifactData : MonoBehaviour
{
    public string ArtifactName;
    public string ArtifactDesc;
    public ArtifactRankDataSO ArtifactRank;
    public int ResaleValue;
    public int SaleValue;
    public Sprite ItemImage;
}
