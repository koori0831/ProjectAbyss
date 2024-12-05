using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BaseNpc : MonoBehaviour
{
    [SerializeField] private ShopItemStorage shopItemStorage;
    [SerializeField] private List<Shop> shopItemInfoTriggers;
    [SerializeField] private DialogSO _baseNpcDialog;

    public void Refill(int price)
    {
        print("gg");
      
        ArtifactData[] artifacts = shopItemStorage.GetArtifactDatasRandom(shopItemInfoTriggers.Count);

        if(MoneySample.Instance.Money >= price)
        {
            MoneySample.Instance.ChangeMoney(-price);
            ChatManager.Instance.StartChat(GetDialog(_baseNpcDialog.JobDialogList), 2f);
            for (int i = 0; i < shopItemInfoTriggers.Count; i++)
            {
                Shop itemInfo = shopItemInfoTriggers[i];
                ArtifactData artifact = artifacts[i];

                itemInfo.artifact = artifact;
                itemInfo.itemSprite.sprite = artifact.ItemImage;
                print($"이름 {artifact.ArtifactName}, 등급 {artifact.ArtifactRank.RankName}");
            }
        }
        else
        {
            ChatManager.Instance.StartChat("돈이 부족해요", 2f);
        }
        
       
    }
    public string GetDialog(List<string> dialogs)
    {
        string dialog = dialogs[Random.Range(0, dialogs.Count)];
        return dialog;
    }



}