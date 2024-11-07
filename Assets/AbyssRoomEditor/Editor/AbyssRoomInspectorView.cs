using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace ProjectAbyss.RoomEditor
{
    [UxmlElement()]
    public partial class AbyssRoomInspectorView : VisualElement
    {
        public Editor editor { get; set; }
        public void DrawView<T>(T target) where T : Object
        {
            Clear();
            Object.DestroyImmediate(editor);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                editor.OnInspectorGUI();
            });
            this.Add(container);
        }
    }
}
