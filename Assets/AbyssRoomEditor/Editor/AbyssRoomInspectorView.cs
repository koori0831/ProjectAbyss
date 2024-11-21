using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace ProjectAbyss.RoomEditor
{
    [UxmlElement()]
    public partial class AbyssRoomInspectorView : VisualElement
    {
        // public Editor editor { get; set; }

        [UxmlAttribute]
        public bool isDrawDefault = false;

        public Action<SerializedObject> onValueChanged;
        public void DrawInspector<T>(T target) where T : UnityEngine.Object
        {
            Clear();
            if (isDrawDefault)
                DrawWithOutCustomEditor(target);
            else
                DrawWithCustomEditor(target);
        }

        private void DrawWithOutCustomEditor<T>(T target) where T : UnityEngine.Object
        {
            SerializedObject serializedObject = new SerializedObject(target);

            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                PropertyField propertyField = new PropertyField(property);
                propertyField.Bind(serializedObject);
                Add(propertyField);
            }
        }

        private void DrawWithCustomEditor<T>(T target) where T : UnityEngine.Object
        {
            InspectorElement inspectorElement = new InspectorElement();
            SerializedObject serializedObject = new SerializedObject(target);
            inspectorElement.Bind(serializedObject);
            Add(inspectorElement);
            inspectorElement.TrackSerializedObjectValue(serializedObject, OnTrackValue);
        }

        public void HideInspector()
        {
            Clear();
        }

        private void OnTrackValue(SerializedObject @object)
        {
            onValueChanged?.Invoke(@object);
        }
    }
}
