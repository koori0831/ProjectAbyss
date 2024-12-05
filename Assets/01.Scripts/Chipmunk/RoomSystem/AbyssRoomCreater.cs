using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssRoomCreater : MonoBehaviour
{
    [field: SerializeField] AbyssCreater abyssCreater { get; set; }
    public Tilemap AbyssTilemap => abyssCreater.AbyssTilemap;
    public Tilemap AbyssPlatformTilemap => abyssCreater.AbyssPlatformTilemap;
    [field: SerializeField] List<AbyssRoomSO> abyssRooms { get; set; }
    [SerializeField] int roomsBetweenSpace = 30;
    [SerializeField] int lastGeneratedY = 0;
    public void CreateRoom(int yPos, int holeWidth)
    {
        if (yPos >= lastGeneratedY - roomsBetweenSpace)
            return;

        AbyssRoomSO room = abyssRooms[Random.Range(0, abyssRooms.Count)];
        bool isLeft = Random.Range(0, 2) == 0;

        for (int x = 0; x < room.mapSize.x; x++)
        {
            for (int y = room.mapSize.y; y > 0; y--)
            {
                {
                    int xPos = x + holeWidth / 2;

                    AbyssTilemap.SetTile(new Vector3Int(isLeft ? -xPos : xPos, y + yPos, 0), null); ;
                }
                Vector2Int pos = new Vector2Int(x, room.mapSize.y - y);
                if (room.blockDatas.TryGetValue(pos, out AbyssRoomBlockData blockData))
                {
                    int xPos = x + holeWidth / 2;
                    if (isLeft)
                    {
                        xPos = -xPos;
                    }
                    Vector2Int worldPos = new Vector2Int(xPos, y + yPos);
                    blockData.OnTiled(this, worldPos);
                }
            }
        }
        lastGeneratedY = yPos;
    }

#if UNITY_EDITOR
    [ContextMenu("AutoSetRooms")]
    public void AutoSetRooms()
    {
        abyssRooms = new List<AbyssRoomSO>();
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:AbyssRoomSO");
        foreach (var guid in guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            AbyssRoomSO room = UnityEditor.AssetDatabase.LoadAssetAtPath<AbyssRoomSO>(path);
            abyssRooms.Add(room);
        }
    }
#endif
}
