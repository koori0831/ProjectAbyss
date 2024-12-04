using UnityEngine;
using UnityEngine.UIElements;

public class AbyssRoomSOEditorCell : VisualElement
{
    public Vector2Int vector2;

    public AbyssRoomSOEditorCell(Vector2Int vector2, int size)
    {
        this.vector2 = vector2;
        style.width = size;
        style.height = size;
    }
    public void SetColor(Color color)
    {
        style.backgroundColor = new StyleColor(color);
    }
}
