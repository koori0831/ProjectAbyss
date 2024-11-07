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
        internal void DrawView()
        {
            this.Clear();
            CreateElement();
            List<AbyssRoomSO> roomSOList = GetRoomSOList(locationTextField.value);
            foreach (AbyssRoomSO roomSO in roomSOList)
            {
                Button button = new Button(() =>
                {
                    OnRoomSelected?.Invoke(roomSO);
                });
                button.text = roomSO.name;
                this.Add(button);
            }
        }
    }
}