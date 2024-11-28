using System;
using UnityEngine;

public abstract class AbyssRoomBlockData : ScriptableObject
{
#if UNITY_EDITOR
    public Color PaletteColor = new Color(1, 1, 1, 0.5f);
#endif
    public string blockName;
    public virtual void OnCreate(AbyssRoomSO target, Vector2Int localPos)
    {
        Vector2Int position = localPos;

        if (target.blockDatas.ContainsKey(position))
        {
            target.blockDatas[position].OnRemove(target, position);
        }

        target.blockDatas.Add(position, this);
    }
    public virtual void OnRemove(AbyssRoomSO target, Vector2Int localPos)
    {
        Vector2Int position = localPos;

        target.blockDatas.Remove(position);
    }
    public abstract void OnTiled(AbyssRoomCreater roomCreater, Vector2Int worldPos);

}