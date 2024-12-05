using UnityEngine;

public class AbyssRoomGameobjectSO : AbyssRoomBlockData
{
    [field: SerializeField] public GameObject GameObject { get; }
    public override void OnTiled(AbyssRoomCreater roomCreater, Vector2Int worldPos)
    {
        GameObject gameObject = Instantiate(GameObject, new Vector3(worldPos.x, worldPos.y), Quaternion.identity);
    }
}
