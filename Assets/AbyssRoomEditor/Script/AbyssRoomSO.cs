using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbyssRoomSO", menuName = "AbyssRoomSO")]
public class AbyssRoomSO : ScriptableObject
{
    public Vector2Int mapSize = new Vector2Int(5, 5);
    public SerializableDictionary<Vector2Int, AbyssRoomBlockData> blockDatas = new();
    public List<AbyssRoomBlockData> childRoomDatas = new();
    #if UNITY_EDITOR
    public void AddChildRoomData(AbyssRoomBlockData data)
    {
        childRoomDatas.Add(data);
    }
    #endif
}
