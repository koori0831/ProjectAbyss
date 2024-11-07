using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;


[CustomEditor(typeof(ItemSO))]
public class CustomItemSO : Editor
{
    private SerializedProperty itemTypeProp;
    private SerializedProperty ammoTypeProp;
    private SerializedProperty minAmountProp;
    private SerializedProperty maxAmountProp;
    private SerializedProperty _prefabProp;
    private SerializedProperty _itemSpriteProp;

    private void OnEnable()
    {
        itemTypeProp = serializedObject.FindProperty("itemType");
        ammoTypeProp = serializedObject.FindProperty("ammoType");
        minAmountProp = serializedObject.FindProperty("minAmount");
        maxAmountProp = serializedObject.FindProperty("maxAmount");
        _prefabProp = serializedObject.FindProperty("prefab");
        _itemSpriteProp = serializedObject.FindProperty("itemSprite");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(itemTypeProp);
            _itemSpriteProp.objectReferenceValue = EditorGUILayout.ObjectField(GUIContent.none, _itemSpriteProp.objectReferenceValue, typeof(Sprite), false, GUILayout.Width(65f));
        }
        EditorGUILayout.EndHorizontal();
        if (itemTypeProp.GetEnumValue<ItemType>() == ItemType.Ammo)
            EditorGUILayout.PropertyField(ammoTypeProp);
        EditorGUILayout.LabelField("MinMAx Value");
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(minAmountProp, GUIContent.none);
            EditorGUILayout.PropertyField(maxAmountProp, GUIContent.none);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(_prefabProp);

        serializedObject.ApplyModifiedProperties();
    }
}
