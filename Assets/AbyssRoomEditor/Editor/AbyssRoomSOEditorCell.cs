using UnityEngine;
using UnityEngine.UIElements;

public class AbyssRoomSOEditorCell : VisualElement
{
    public Vector2Int vector2;

    public AbyssRoomSOEditorCell(Vector2Int vector2)
    {
        this.vector2 = vector2;
    }
    public void SetColor(Color color)
    {
        style.backgroundColor = new StyleColor(color);
    }
}
