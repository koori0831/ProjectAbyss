using System;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectAbyss.RoomEditor
{

    [UxmlElement()]
    public partial class AbyssRoomPaletteView : VisualElement
    {
        AbyssRoomEditorWindow Window { get; set; }

        [UxmlAttribute]
        string palettePath = "Assets/AbyssRoomEditor/ScriptableObject/Palette";
        public AbyssRoomPalette SelectedPalette => paletteField.value as AbyssRoomPalette;
        UnityEditor.UIElements.ObjectField paletteField;
        Button paletteCreateBtn;
        VisualElement paletteItemContainer;

        public AbyssRoomPaletteItemView selectedPaletteItem;
        public AbyssRoomPaletteItemView SelectedPaletteItem
        {
            get => selectedPaletteItem;
            private set
            {
                if (selectedPaletteItem != null)
                {
                    selectedPaletteItem.RemoveFromClassList("selected");
                }
                selectedPaletteItem = value;
                if (selectedPaletteItem != null)
                {
                    selectedPaletteItem.SendToBack();
                    selectedPaletteItem.AddToClassList("selected");
                }
            }
        }
        public AbyssRoomPaletteView()
        {
            paletteField = new UnityEditor.UIElements.ObjectField();
            paletteField.objectType = typeof(AbyssRoomPalette);
            // paletteField.ty = typeof(AbyssRoomPalette);
            this.Add(paletteField);

            paletteCreateBtn = new Button();
            paletteCreateBtn.text = "Create";
            paletteCreateBtn.clicked += OnCreatePalette;
            paletteField.RegisterValueChangedCallback(OnPaletteChange);
            this.Add(paletteCreateBtn);

            paletteItemContainer = new VisualElement();
            paletteItemContainer.name = "PaletteItemContainer";
            this.Add(paletteItemContainer);

            Selection.selectionChanged += OnSelectionChanged;
        }
        public void DrawView(AbyssRoomEditorWindow window)
        {
            this.Window = window;
        }

        private void OnPaletteChange(ChangeEvent<UnityEngine.Object> evt)
        {
            Debug.Log("Palette Changed");

            AbyssRoomPalette palette = evt.newValue as AbyssRoomPalette;
            if (palette == null)
                return;
            palette.valueChanged += DrawPaletteItem;
            DrawPaletteItem(palette);
        }

        private void DrawPaletteItem(AbyssRoomPalette palette)
        {
            paletteItemContainer.Clear();
            foreach (AbyssRoomBlockData blockData in palette.Values)
            {
                AbyssRoomPaletteItemView itemView = new AbyssRoomPaletteItemView(blockData);
                itemView.OnClickInspector = (data) =>
                {
                    Window.InspectorView.DrawInspector(data);
                    Window.InspectorView.onValueChanged = (blockData) =>
                    {
                        DrawPaletteItem(palette);
                    };
                };
                itemView.OnClick = (data) =>
                {
                    SelectedPaletteItem = itemView;
                };
                paletteItemContainer.Add(itemView);
            }
        }

        private void OnCreatePalette()
        {
            AbyssRoomPalette palette = ScriptableObject.CreateInstance<AbyssRoomPalette>();
            if (AssetDatabase.LoadAssetAtPath<ScriptableObject>($"{palettePath}/NewPalette.asset") != null)
            {
                int count = 0;
                while (AssetDatabase.LoadAssetAtPath<ScriptableObject>($"{palettePath}/NewPalette {count}.asset") != null)
                {
                    count++;
                }
                AssetDatabase.CreateAsset(palette, $"{palettePath}/NewPalette {count}.asset");
            }
            else
                AssetDatabase.CreateAsset(palette, $"{palettePath}/NewPalette.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            paletteField.value = palette;
        }



        private void OnSelectionChanged()
        {
            if (Selection.activeObject is AbyssRoomPalette)
            {
                paletteField.value = Selection.activeObject;
            }
        }
        ~AbyssRoomPaletteView()
        {
            Selection.selectionChanged -= OnSelectionChanged;
        }
    }

}