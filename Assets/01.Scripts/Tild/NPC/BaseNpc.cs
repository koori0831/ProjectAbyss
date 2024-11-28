using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BaseNpc : MonoBehaviour
{
    [SerializeField] private ShopItemStorage shopItemStorage;
    [SerializeField] private List<Shop> shopItemInfoTriggers;

    public void Refill()
    {
        print("gg");
        // 필요한 artifact 데이터의 개수를 가져옴
        ArtifactData[] artifacts = shopItemStorage.GetArtifactDatasRandom(shopItemInfoTriggers.Count);

        // shopItemInfoTriggers와 artifacts를 1:1로 매핑하여 설정
        for (int i = 0; i < shopItemInfoTriggers.Count; i++)
        {
            Shop itemInfo = shopItemInfoTriggers[i];
            ArtifactData artifact = artifacts[i];

            itemInfo.artifact = artifact;
            itemInfo.itemSprite.sprite = artifact.ItemImage;
            print($"이름 {artifact.ArtifactName}, 등급 {artifact.ArtifactRank.RankName}");
        }
    }




}