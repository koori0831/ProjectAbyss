using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AbyssRoomPalette : ScriptableObject
{
    public List<AbyssRoomBlockData> Values = new();
#if UNITY_EDITOR
    public Action<AbyssRoomPalette> valueChanged;
    public void AddValue(AbyssRoomBlockData data)
    {
        Values.Add(data);
        valueChanged?.Invoke(this);

        AssetDatabase.AddObjectToAsset(data, this);
        AssetDatabase.SaveAssets();
    }
    public void RemoveValue(AbyssRoomBlockData data)
    {
        Values.Remove(data);
        valueChanged?.Invoke(this);

        AssetDatabase.RemoveObjectFromAsset(data);
        AssetDatabase.SaveAssets();
    }
    private void OnValidate()
    {

    }
#endif
}
