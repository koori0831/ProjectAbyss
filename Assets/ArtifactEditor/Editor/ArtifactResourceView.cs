using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ArtifactResourceView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ArtifactResourceView, VisualElement.UxmlTraits> { }
        private Dictionary<Type, ArtifactResourceFold> resourceFoldDic = new();
        public Action<ArtifactSO> onCreateItem;
        public ArtifactResourceView()
        {
            ReloadView();
        }
        public void ReloadView()
        {
            this.Clear();
            CreateFold(typeof(ArtifactSO));
        }
        public void CreateFold(Type type)
        {
            this.Clear();
            resourceFoldDic.Clear();

            if (!type.IsAbstract)
            {
                ArtifactResourceFold resourceFold = CreateArtifactResourceFold(type);
                resourceFoldDic.Add(type, resourceFold);
                this.Add(resourceFold);
            }

            TypeCache.TypeCollection typeCollect = TypeCache.GetTypesDerivedFrom(type);
            foreach (Type cachedType in typeCollect)
            {
                ArtifactResourceFold resourceFold = CreateArtifactResourceFold(cachedType);
                resourceFoldDic.Add(cachedType, resourceFold);
            }

            foreach (Type cachedType in typeCollect)
            {
                if (cachedType == type)
                {
                    Debug.Log("같네?");
                    continue;
                }
                if (resourceFoldDic.ContainsKey(cachedType.BaseType))
                {
                    resourceFoldDic[cachedType.BaseType].element.Add(resourceFoldDic[cachedType]);
                }
                else
                {
                    this.Add(resourceFoldDic[cachedType]);
                }
            }

        }

        private ArtifactResourceFold CreateArtifactResourceFold(Type cachedType)
        {
            ArtifactResourceFold itemResourceFold = new ArtifactResourceFold();
            itemResourceFold.Initialize(cachedType);
            itemResourceFold.onClick += CreateItem;
            return itemResourceFold;
        }

        public void CreateItem(Type type)
        {
            ArtifactSO itemSO = ScriptableObject.CreateInstance(type) as ArtifactSO;

            Undo.RegisterCreatedObjectUndo(itemSO, "ArtifactEditor Create Item");

            SaveItem(itemSO, $"Assets/ArtifactEditor/ScriptableObject/{type.ToString()}");
            onCreateItem?.Invoke(itemSO);
        }
        private void SaveItem(ArtifactSO artifactSO, string path)
        {
            ArtifactSO finedSO = AssetDatabase.LoadAssetAtPath<ArtifactSO>($"{path}.asset");
            int count = 0;
            if (finedSO == null)
            {
                AssetDatabase.CreateAsset(artifactSO, $"{path}.asset");
                AssetDatabase.SaveAssets();
                return;
            }
            else
            {
                while (finedSO != null)
                {
                    count++;
                    finedSO = AssetDatabase.LoadAssetAtPath<ArtifactSO>($"{path} {count}.asset");
                }
            }
            AssetDatabase.CreateAsset(artifactSO, $"{path} {count}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}