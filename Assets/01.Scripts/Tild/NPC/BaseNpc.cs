using UnityEngine;
using System.Collections.Generic;

public class BaseNpc : MonoBehaviour
{
    [SerializeField] private ShopItemStorage shopItemStorage;
    [SerializeField] private List<Shop> shopItemInfoTriggers;
    [SerializeField] private DialogSO _baseNpcDialog;

    public void Refill(int price)
    {
        if (shopItemStorage == null || shopItemInfoTriggers == null || shopItemInfoTriggers.Count == 0)
        {
            Debug.LogError("ShopItemStorage 또는 ShopItemInfoTriggers가 설정되지 않았습니다.");
            return;
        }

        if (MoneySample.Instance.Money < price)
        {
            ChatManager.Instance.StartChat("돈이 부족해요", 2f);
            return;
        }

        MoneySample.Instance.ChangeMoney(-price);
        ChatManager.Instance.StartChat(GetDialog(_baseNpcDialog.JobDialogList), 2f);

        // Random으로 아이템 데이터를 가져옴
        ArtifactSO[] artifacts = shopItemStorage.GetArtifactDatasRandom(shopItemInfoTriggers.Count);

        if (artifacts == null || artifacts.Length < shopItemInfoTriggers.Count)
        {
            Debug.LogError("ShopItemStorage에서 충분한 Artifact 데이터를 가져오지 못했습니다.");
            return;
        }

        // ShopItemInfoTriggers에 Artifact 데이터를 할당
        for (int i = 0; i < shopItemInfoTriggers.Count; i++)
        {
            Shop itemInfo = shopItemInfoTriggers[i];
            ArtifactSO artifact = artifacts[i];

            if (itemInfo != null && artifact != null)
            {
                itemInfo.artifact = artifact;
                itemInfo.itemSprite.sprite = artifact.ItemImage;

                Debug.Log($"이름: {artifact.ArtifactName}, 등급: {artifact.ArtifactRank.RankName}");
            }
            else
            {
                Debug.LogWarning($"ShopItemInfoTrigger 또는 Artifact가 null입니다. Index: {i}");
            }
        }
    }

    public string GetDialog(List<string> dialogs)
    {
        if (dialogs == null || dialogs.Count == 0)
        {
            return "대화가 없습니다.";
        }

        string dialog = dialogs[Random.Range(0, dialogs.Count)];
        return dialog;
    }
}
