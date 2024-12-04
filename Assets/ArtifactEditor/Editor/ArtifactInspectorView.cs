using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ArtifactInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ArtifactInspectorView, VisualElement.UxmlTraits> { }
        Editor editor;
        public Action onDataChange;
        public void UpdateInspactor(ArtifactSO artifactSO)
        {
            this.Clear();
            UnityEngine.Object.DestroyImmediate(editor);

            if (artifactSO == null) return;

            editor = Editor.CreateEditor(artifactSO);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target != null)
                    editor.OnInspectorGUI();
            });
            BindingExtensions.TrackSerializedObjectValue(container, editor.serializedObject, serialzedObject => onDataChange?.Invoke());
            Add(container);
        }
    }
}