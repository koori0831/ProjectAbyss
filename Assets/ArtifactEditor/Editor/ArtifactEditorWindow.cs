using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ItemEditorWindow : EditorWindow
    {
        [SerializeField] VisualTreeAsset visualTree;
        ArtifactSplitView itemSplitView;
        ArtifactInspectorView itemInspectorView;
        ArtifactEditorView itemEditorView;
        ArtifactResourceView itemResourceView;
        VisualElement reloadBtn;

        [MenuItem("Chipmunk/ItemWindow")]
        public static void OnOpenWindow()
        {
            ItemEditorWindow window = GetWindow<ItemEditorWindow>();
            window.titleContent = new GUIContent("ItemEditor");

        }

        private void OnUndoRedo()
        {
            OnReload();
        }

        public void CreateGUI()
        {
            VisualElement rootEle = rootVisualElement;

            // VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ItemEditor/Editor/ItemEditorWindow.uxml");
            visualTree.CloneTree(rootEle);

            itemSplitView = rootEle.Q<ArtifactSplitView>();

            itemInspectorView = QuearyOrAction<ArtifactInspectorView>(itemSplitView, () => new ArtifactInspectorView());
            itemResourceView = QuearyOrAction<ArtifactResourceView>(itemSplitView, () => new ArtifactResourceView());
            itemEditorView = QuearyOrAction<ArtifactEditorView>(itemSplitView, () => new ArtifactEditorView());

            itemResourceView.onCreateItem += OnSelectItem;
            itemResourceView.onCreateItem += itemEditorView.ReFreshView;
            itemEditorView.Initialize();
            itemEditorView.onSelect += OnSelectItem;
            itemInspectorView.onDataChange += OnDataChange;



            reloadBtn = rootEle.Q("ReloadBtn");
            reloadBtn.RegisterCallback<ClickEvent>(OnReload);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnDataChange()
        {
            itemEditorView.ReFreshView();
        }

        private void OnReload(ClickEvent evt)
        {
            Debug.Log("mingming이 왔어요~!");
            OnReload();
        }
        private void OnReload()
        {
            itemEditorView.ReFreshView();
            itemResourceView.ReloadView();
        }
        public void OnSelectItem(ArtifactSO artifactSO)
        {
            itemInspectorView.UpdateInspactor(artifactSO);
        }
        public static T QuearyOrAction<T>(VisualElement root, Func<T> action) where T : VisualElement
        {
            T find = root.Q<T>();
            if (find == null)
                find = action.Invoke();
            if (root.Q<T>() == null)
                root.Add(find);
            return find;
        }
    }
}