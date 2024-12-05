using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssRoomTileSO : AbyssRoomBlockData
{
    [field: SerializeField] public TileBase TileBase { get; private set; }
    [SerializeField] private bool isPlatform = false;
    public override void OnTiled(AbyssRoomCreater roomCreater, Vector2Int worldPos)
    {
        if (isPlatform)
            roomCreater.AbyssPlatformTilemap.SetTile(new Vector3Int(worldPos.x, worldPos.y, 0), TileBase);
        else
            roomCreater.AbyssTilemap.SetTile(new Vector3Int(worldPos.x, worldPos.y, 0), TileBase);
    }
}
