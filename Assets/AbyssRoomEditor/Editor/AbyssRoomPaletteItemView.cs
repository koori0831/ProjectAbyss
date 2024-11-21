using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AbyssRoomPaletteItemView : VisualElement
{
    public AbyssRoomBlockData Data { get; private set; }
    public Action<AbyssRoomBlockData> OnClickInspector { get; set; }
    public Action<AbyssRoomPaletteItemView> OnClick { get; set; }
    public AbyssRoomPaletteItemView(AbyssRoomBlockData data)
    {
        Data = data;
        CreateElement(data);
    }

    private void CreateElement(AbyssRoomBlockData data)
    {
        VisualElement Color = new VisualElement();
        Color.name = "Color";
        Color.style.backgroundColor = data.PaletteColor;
        this.Add(Color);
        Label label = new Label();
        if (data.blockName == null || data.blockName == "")
            label.text = data.name;
        else
            label.text = data.blockName;

        this.Add(label);

        VisualElement openInspectorBtn = new VisualElement();
        openInspectorBtn.name = "OpenInspectorBtn";
        openInspectorBtn.RegisterCallback<ClickEvent>(OnClickInspectorBtn);
        this.Add(openInspectorBtn);

        this.RegisterCallback<ClickEvent>(OnClickHandler);
    }

    private void OnClickHandler(ClickEvent evt)
    {
        OnClick?.Invoke(this);
    }

    private void OnClickInspectorBtn(ClickEvent evt)
    {
        OnClickInspector?.Invoke(Data);
    }
}
