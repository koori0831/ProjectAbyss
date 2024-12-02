using System;
using System.Collections.Generic;
using ProjectAbyss.RoomEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CustomEditor(typeof(AbyssRoomSO))]
public class AbyssRoomSOEditor : Editor
{
    public VisualTreeAsset visualTreeAsset;

    VisualElement root;
    AbyssRoomSO roomSO;

    AbyssRoomPaletteView palette;
    Dictionary<Vector2Int, AbyssRoomSOEditorCell> cells = new Dictionary<Vector2Int, AbyssRoomSOEditorCell>();
    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();
        visualTreeAsset.CloneTree(root);

        roomSO = target as AbyssRoomSO;

        DrawRoom(roomSO);
        Initailize(roomSO);

        return root;
    }

    private void DrawRoom(AbyssRoomSO roomSO)
    {
        VisualElement roomView = root.Q<VisualElement>("RoomView");
        roomView.Clear();
        cells.Clear();
        for (int y = 0; y < roomSO.mapSize.y; y++)
        {
            VisualElement rowContainer = new VisualElement();
            rowContainer.name = "RowContainer";
            rowContainer.style.flexDirection = FlexDirection.Row;
            for (int x = 0; x < roomSO.mapSize.x; x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                AbyssRoomSOEditorCell cell = new AbyssRoomSOEditorCell(position);
                cell.RegisterCallback<MouseDownEvent>(MouseDownHandler);
                cell.name = "Block";
                rowContainer.Add(cell);

                cells.Add(position, cell);
                if (roomSO.blockDatas.TryGetValue(position, out AbyssRoomBlockData blockData))
                    cell.SetColor(blockData.PaletteColor);
            }
            roomView.Add(rowContainer);
        }
    }

    private void MouseDownHandler(MouseDownEvent evt)
    {

        AbyssRoomPaletteView abyssRoomPaletteView = GetRootElement(root).Q<AbyssRoomPaletteView>();
        if (abyssRoomPaletteView == null)
            return;

        AbyssRoomSOEditorCell cell = evt.target as AbyssRoomSOEditorCell;
        if (evt.button == 0)
        {
            if (abyssRoomPaletteView.SelectedPaletteItem == null)
                return;
            Debug.Log("Create");
            abyssRoomPaletteView.SelectedPaletteItem.Data.OnCreate(roomSO, cell.vector2);
        }
        if (evt.button == 1)
        {
            Debug.Log("Remove");
            if (roomSO.blockDatas.TryGetValue(cell.vector2, out AbyssRoomBlockData blockData))
            {
                blockData.OnRemove(roomSO, cell.vector2);
            }
        }

        foreach (var item in cells)
        {
            if (roomSO.blockDatas.TryGetValue(item.Key, out AbyssRoomBlockData blockData))
            {
                item.Value.SetColor(blockData.PaletteColor);
            }
            else
            {
                item.Value.SetColor(new Color(33 / 255f, 33 / 255f, 33 / 255f, 1));
            }
        }
    }

    public void SetPalette(AbyssRoomPaletteView palette)
    {
        this.palette = palette;
    }

    private void Initailize(AbyssRoomSO roomSO)
    {
        Button reloadBtn = root.Q<Button>("Reload");
        reloadBtn.clicked += () =>
        {
            DrawRoom(roomSO);
        };

        root.Q<AbyssRoomInspectorView>().DrawInspector(roomSO);
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
