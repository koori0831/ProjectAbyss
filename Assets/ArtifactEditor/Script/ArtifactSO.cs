using UnityEngine;

public class ArtifactSO : ScriptableObject
{
    [field: SerializeField] public string ArtifactName { get; private set; }
    [field: SerializeField] public string ArtifactDesc { get; private set; }
    [field: SerializeField] public ArtifactRankDataSO ArtifactRank { get; private set; }
    [field: SerializeField] public Sprite ItemImage { get; private set; }
    [field: SerializeField] public int ResaleValue { get; private set; }
    [field: SerializeField] public int SaleValue { get; private set; }
    public virtual ArtifactSO Clone()
    {
        return Instantiate(this);
    }
}
