using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectAbyss.RoomEditor
{
    [UxmlElement()]
    public partial class AbyssRoomListView : VisualElement
    {
        public AbyssRoomEditorWindow Window { get; private set; }
        [UxmlAttribute("Folder Field")]
        public TextField locationTextField;
        public Button locateButton;
        public Action<AbyssRoomSO> OnRoomSelected;
        public AbyssRoomListView()
        {
            DrawView();
        }

        private void CreateElement()
        {
            VisualElement header = new VisualElement();
            header.name = "Header";
            header.style.justifyContent = Justify.SpaceBetween;
            this.Add(header);


            locationTextField = new TextField("Folder Location");
            locationTextField.Q<Label>().style.minWidth = 0;
            locationTextField.value = "Assets/AbyssRoomEditor/ScriptableObject";
            header.Add(locationTextField);

            locateButton = new Button(() =>
            {
                DrawView();
            });
            locateButton.text = "Locate";
            header.Add(locateButton);
        }

        public List<AbyssRoomSO> GetRoomSOList(string path)
        {
            List<AbyssRoomSO> roomSOList = new List<AbyssRoomSO>();
            string[] guids = AssetDatabase.FindAssets("t:AbyssRoomSO", new string[] { path });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                AbyssRoomSO roomSO = AssetDatabase.LoadAssetAtPath<AbyssRoomSO>(assetPath);
                if (roomSO != null)
                {
                    roomSOList.Add(roomSO);
                }
            }
            return roomSOList;
        }
        Button selectedBtn;
        public void DrawView(AbyssRoomEditorWindow window)
        {
            this.Window = window;
            DrawView();
        }
        private void DrawView()
        {
            this.Clear();
            CreateElement();
            List<AbyssRoomSO> roomSOList = GetRoomSOList(locationTextField.value);
            foreach (AbyssRoomSO roomSO in roomSOList)
            {
                Button button = new Button();
                button.text = roomSO.name;
                this.Add(button);

                button.RegisterCallback<ClickEvent>((evt) =>
                {
                    ClickCallback(roomSO, button);
                });
            }
        }

        private void ClickCallback(AbyssRoomSO roomSO, Button button)
        {
            Selection.activeObject = roomSO;
            
            AbyssRoomInspectorView inspectorView = GetRootElement(this).Query<AbyssRoomInspectorView>().First();
            inspectorView.DrawInspector(roomSO);

            if (selectedBtn != null)
            {
                selectedBtn.RemoveFromClassList("selected");
            }
            selectedBtn = button;
            selectedBtn.AddToClassList("selected");
        }
        public static VisualElement GetRootElement(VisualElement element)
        {
            VisualElement currentElement = element;
            while (currentElement.hierarchy.parent != null)
            {
                currentElement = currentElement.hierarchy.parent;
            }
            return currentElement;
        }
    }
}