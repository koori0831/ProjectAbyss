using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssRoomCreater : MonoBehaviour
{
    [field: SerializeField] public Tilemap AbyssTilemap { get; private set; }
    [field: SerializeField] List<AbyssRoomSO> abyssRooms { get; set; }
    public void CreateRoom()
    {

    }
}
