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

        AbyssRoomInspectorView inspectorView;
        AbyssRoomListView listView;
        AbyssRoomCreateToolsView createToolsView;


        [MenuItem("Editor/AbyssRoomEditor")]
        public static void OpenWindow()
        {
            AbyssRoomEditorWindow wnd = GetWindow<AbyssRoomEditorWindow>();
            wnd.titleContent = new GUIContent("AbyssRoomEditor");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            m_VisualTreeAsset.CloneTree(root);
            
            inspectorView = root.Q<AbyssRoomInspectorView>("inspector-view");
            listView = root.Q<AbyssRoomListView>("list-view");
            createToolsView = root.Q<AbyssRoomCreateToolsView>("create-tools-view");

            listView.OnRoomSelected = (roomSO) =>
            {
                inspectorView.DrawView(roomSO);
            };

            listView.DrawView();
            createToolsView.DrawView();
        }
    }
}