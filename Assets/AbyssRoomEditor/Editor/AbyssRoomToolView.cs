using System;
using ProjectAbyss.RoomEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AbyssRoomToolView : VisualElement
{
    Type type;
    public VisualElement element { get; private set; }
    AbyssRoomCreateToolsView toolsView;
    public AbyssRoomToolView(AbyssRoomCreateToolsView toolsView)
    {
        this.toolsView = toolsView;
    }
    public void DrawView(Type type)
    {


        this.type = type;
        DrawElement(type);
        DrawButton(type);
    }

    private void DrawButton(Type type)
    {
        if (type.IsAbstract)
            return;

        Button addButton = new();
        addButton.text = "Create";
        addButton.AddToClassList("ResourceAdd");

        addButton.RegisterCallback<ClickEvent>(OnClick);
        this.Add(addButton);
    }

    private void DrawElement(Type type)
    {
        bool isRoot = TypeCache.GetTypesDerivedFrom(type).Count == 0;
        if (isRoot)
        {
            Label label = new Label();
            label.text = $"     {type.ToString()}";
            element = label;
        }
        else
        {
            Foldout foldout = new Foldout();
            foldout.text = $"{type.ToString()}";
            element = foldout;
        }
        element.AddToClassList("ResourceView");
        this.Add(element);
    }
    private void OnClick(ClickEvent evt)
    {
        AbyssRoomBlockData scriptableObject = ScriptableObject.CreateInstance(type) as AbyssRoomBlockData;
        scriptableObject.name = type.ToString();

        toolsView.SaveScriptableObject(scriptableObject);
    }

}
