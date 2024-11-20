using UnityEngine;

public enum ArtifactRank
{
    None,
    Special,
    High,
    normal,
    Low
}

public abstract class ArtifactData : MonoBehaviour
{
    public string ArtifactName;
    public string ArtifactDesc;
    public ArtifactRank artifactRank;
    public int ResaleValue;
    public int SaleValue;
    public Sprite ItemImage;
}
