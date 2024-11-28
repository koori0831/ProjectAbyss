using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssRoomTileSO : AbyssRoomBlockData
{
    [field: SerializeField] public TileBase TileBase { get; private set; }
    public override void OnTiled(AbyssRoomCreater roomCreater, Vector2Int worldPos)
    {
        roomCreater.AbyssTilemap.SetTile(new Vector3Int(worldPos.x, worldPos.y, 0), TileBase);
    }
}
