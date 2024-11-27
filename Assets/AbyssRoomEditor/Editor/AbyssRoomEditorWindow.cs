using System;
using ProjectAbyss.RoomEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectAbyss.RoomEditor
{
    public class AbyssRoomEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        public AbyssRoomInspectorView InspectorView { get; private set; }
        public AbyssRoomListView RoomListView { get; private set; }
        public AbyssRoomCreateToolsView CreateToolsView { get; private set; }
        public AbyssRoomPaletteView PaletteView { get; private set; }


        private AbyssRoomSO selectedRoomSO;
        public AbyssRoomSO SelectedRoomSO
        {
            get
            {
                return selectedRoomSO;
            }
            set
            {
                selectedRoomSO = value;
                InspectorView.DrawInspector(selectedRoomSO);
            }
        }
        [MenuItem("Editor/AbyssRoomEditor")]
        public static void OpenWindow()
        {
            // Selection.selectionChanged += OnSelectionChanged;
        
            AbyssRoomEditorWindow wnd = GetWindow<AbyssRoomEditorWindow>();
            wnd.titleContent = new GUIContent("AbyssRoomEditor");
        }

        // private static void OnSelectionChanged()
        // {
        //     Selection.activeObject
        // }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            m_VisualTreeAsset.CloneTree(root);

            InspectorView = root.Q<AbyssRoomInspectorView>();
            RoomListView = root.Q<AbyssRoomListView>();
            CreateToolsView = root.Q<AbyssRoomCreateToolsView>();
            PaletteView = root.Q<AbyssRoomPaletteView>();

            RoomListView.DrawView(this);
            RoomListView.OnRoomSelected?.Invoke(null);
            CreateToolsView.DrawView(this);
            PaletteView.DrawView(this);
        }
    }
}