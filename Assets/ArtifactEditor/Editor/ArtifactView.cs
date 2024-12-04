using System;
using System.Collections;
using System.Collections.Generic;
using DG.DemiEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chipmunk.ArtifactEditor
{
    public class ArtifactView : VisualElement
    {
        public ArtifactSO ArtifactSO { get; private set; }
        VisualElement rootElement;
        VisualElement itemSpriteElement;
        Label itemName;
        Label itemRarity;
        Button deleteBtn;
        Label itemType;
        public ArtifactView()
        {
            rootElement = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ArtifactEditor/Editor/ArtifactView.uxml").Instantiate().Q<VisualElement>("ItemView");

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ArtifactEditor/Editor/ArtifactView.uss");
            rootElement.styleSheets.Add(styleSheet);

            this.Add(rootElement);

            itemSpriteElement = rootElement.Q("ItemSprite");
            itemName = rootElement.Q<Label>("ItemName");
            itemRarity = rootElement.Q<Label>("ItemRarity");
            deleteBtn = rootElement.Q<Button>("DeleteButton");
            itemType = rootElement.Q<Label>("ItemType");
        }
        public Action<ArtifactView> onClick;
        public Action<ArtifactView> onDeleteButtonClick;
        public void Initialize(ArtifactSO itemSO)
        {
            this.ArtifactSO = itemSO;
            {
                var img = itemSpriteElement.style.backgroundImage;
                Background background = itemSpriteElement.style.backgroundImage.value;
                background.sprite = itemSO.ItemImage;
                img.value = background;
                itemSpriteElement.style.backgroundImage = img;
            }
            {
                itemName.text = itemSO.ArtifactName.IsNullOrEmpty() ? "None" : itemSO.ArtifactName;
            }
            {

                if (itemSO.ArtifactRank != null)
                {
                    string rarity = itemSO.ArtifactRank == null ? "None" : itemSO.ArtifactRank.name;
                    itemRarity.text = rarity;
                    itemRarity.style.color = itemSO.ArtifactRank == null ? Color.gray : itemSO.ArtifactRank.RankColor;

                    rootElement.AddToClassList(rarity);
                }
            }
            {
                itemType.text = itemSO.GetType().Name;
            }

            deleteBtn.RegisterCallback<ClickEvent>(evt => OnDeleteClick());
            RegisterCallback<ClickEvent>(evt => OnClick());
        }

        private void OnDeleteClick()
        {
            onDeleteButtonClick.Invoke(this);
        }

        public void AddClass(string className)
        {
            rootElement.AddToClassList(className);
        }
        public void RemoveClass(string className)
        {
            rootElement.RemoveFromClassList(className);
        }

        private void OnClick()
        {
            onClick?.Invoke(this);
        }
    }
}