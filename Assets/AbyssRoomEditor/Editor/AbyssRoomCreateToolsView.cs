using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectAbyss.RoomEditor
{
    [UxmlElement()]
    public partial class AbyssRoomCreateToolsView : VisualElement
    {
        public AbyssRoomEditorWindow Window { get; private set; }
        [UxmlAttribute]
        string palettePath = "Assets/AbyssRoomEditor/ScriptableObject/Palette";
        public Dictionary<Type, AbyssRoomToolView> entitySOFolds = new();
        public void DrawView(AbyssRoomEditorWindow window)
        {
            this.Window = window;
            Type baseType = typeof(AbyssRoomBlockData);
            entitySOFolds.Clear();

            TypeCache.TypeCollection typeCollect = TypeCache.GetTypesDerivedFrom(baseType);
            foreach (Type cachedType in typeCollect)
            {
                AbyssRoomToolView itemResourceFold = new AbyssRoomToolView(this);
                itemResourceFold.DrawView(cachedType);
                entitySOFolds.Add(cachedType, itemResourceFold);
            }

            foreach (Type cachedType in typeCollect)
            {
                if (cachedType == baseType)
                {
                    continue;
                }
                if (entitySOFolds.ContainsKey(cachedType.BaseType))
                {
                    entitySOFolds[cachedType.BaseType].element.Add(entitySOFolds[cachedType]);
                }
                else
                {
                    this.Add(entitySOFolds[cachedType]);
                }
            }

        }
        public void SaveScriptableObject(AbyssRoomBlockData scriptableObject)
        {
            Window.PaletteView.SelectedPalette.AddValue(scriptableObject);

            // AbyssRoomBlockData finedSO = AssetDatabase.LoadAssetAtPath<AbyssRoomBlockData>($"{path}.asset");
            // int count = 0;
            // if (finedSO == null)
            // {
            //     AssetDatabase.CreateAsset(scriptableObject, $"{path}.asset");
            //     AssetDatabase.SaveAssets();
            //     return;
            // }
            // else
            // {
            //     while (finedSO != null)
            //     {
            //         count++;
            //         finedSO = AssetDatabase.LoadAssetAtPath<AbyssRoomBlockData>($"{path} {count}.asset");
            //     }
            // }
            // AssetDatabase.CreateAsset(scriptableObject, $"{path} {count}.asset");
            // AssetDatabase.SaveAssets();
        }
    }
}