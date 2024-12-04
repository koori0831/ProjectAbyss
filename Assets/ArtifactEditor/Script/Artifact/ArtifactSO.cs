using Sirenix.OdinInspector;
using UnityEngine;

public class ArtifactSO : ScriptableObject
{
    [Header("Artifact Info")]
    [field: SerializeField] public string ArtifactName { get; private set; }
    [field: SerializeField] public string ArtifactDesc { get; private set; }
    [field: SerializeField] public ArtifactRankDataSO ArtifactRank { get; private set; }
    [field: SerializeField] public Sprite ItemImage { get; private set; }
    [field: SerializeField] public int ResaleValue { get; private set; }
    [field: SerializeField] public int SaleValue { get; private set; }
    [ContextMenu("Clone")]
    public virtual ArtifactSO Clone()
    {
        return Instantiate(this);
    }
}
