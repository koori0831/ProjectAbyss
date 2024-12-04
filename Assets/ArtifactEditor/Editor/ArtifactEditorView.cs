using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ArtifactEditorView : VisualElement
    {
        public ArtifactEditorView()
        {
        }
        public new class UxmlFactory : UxmlFactory<ArtifactEditorView, VisualElement.UxmlTraits> { }
        // public Action i
        List<ArtifactSO> ArtifactSOList = new();
        public ScrollView listView;
        public Action<ArtifactSO> onSelect;
        ArtifactView selectedItemView;
        public void Initialize()
        {
            listView = this.Q<ScrollView>();
            ReFreshView();
        }
        public void ReFreshView(ArtifactSO itemSO)
        {
            ReFreshData();

            listView.Clear();

            ArtifactSOList.ForEach(item =>
            {
                ArtifactView itemView = new ArtifactView();
                itemView.Initialize(item);
                itemView.onClick += OnSelect;
                itemView.onDeleteButtonClick += OnDelete;
                listView.Add(itemView);

                if (itemSO != null && item == itemSO)
                {
                    OnSelect(itemView);
                }
            });
        }


        public void ReFreshView()
        {
            ReFreshView(null);
        }

        private void OnDelete(ArtifactView view)
        {
            listView.Remove(view);
        }
        private void OnSelect(ArtifactView itemView)
        {
            if (selectedItemView != null)
                selectedItemView.RemoveClass("Selected");

            onSelect?.Invoke(itemView.ArtifactSO);

            Selection.activeObject = itemView.ArtifactSO;

            selectedItemView = itemView;
            itemView.AddClass("Selected");
        }

        public void ReFreshData()
        {
            ArtifactSOList.Clear();
            AssetDatabase.FindAssets("", new[] { "Assets/ArtifactEditor/ScriptableObject" }).ToList().ForEach((Action<string>)(guid =>
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ArtifactSO itemSO = AssetDatabase.LoadAssetAtPath<ArtifactSO>(path);
                if (itemSO != null)
                {
                    ArtifactSOList.Add(itemSO);
                    if (itemSO.name != itemSO.ArtifactName)
                    {
                        RenameSO(path, itemSO.ArtifactName);
                    }
                }
            }));
        }

        private void RenameSO(string path, string name)
        {
            string dirPath = Path.GetDirectoryName(path);
            ScriptableObject finedSO = AssetDatabase.LoadAssetAtPath<ScriptableObject>($"{dirPath}/{name}.asset");
            int count = 0;
            if (finedSO == null)
            {
                AssetDatabase.RenameAsset(path, name);
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
            AssetDatabase.RenameAsset(path, $"{name} {count}");
        }
    }
}