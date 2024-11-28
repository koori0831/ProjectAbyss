using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BaseNpc : MonoBehaviour
{
    [SerializeField] private ShopItemStorage shopItemStorage;
    [SerializeField] private List<ShopItemInfoTrigger> shopItemInfoTriggers;

    public void Refill()
    {
        print("gg");
        ArtifactData[] artifacts = shopItemStorage.GetArtifactDatasRandom(shopItemInfoTriggers.Count);

        foreach (var iteminfo in shopItemInfoTriggers)
        {


            foreach (var artifact in artifacts)
            {
                iteminfo.artifact = artifact;
                iteminfo.itemSprite.sprite = artifact.ItemImage;
                print($"이름 {artifact.ArtifactName}, 등급 {artifact.ArtifactRank.RankName}");
            }
        }


    }



}