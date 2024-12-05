using UnityEngine;
using System.Collections.Generic;

public class ShopItemStorage : MonoBehaviour
{
    public List<ArtifactSO> artifactDatas; // 전체 아티팩트 데이터 리스트
    public List<ArtifactRankDataSO> artifactRankDatas; // 등급 데이터 (확률 포함)

    private Dictionary<ArtifactRank, List<ArtifactSO>> rankedArtifactLists; // ArtifactRank(enum) 기반으로 나눈 리스트

    void Start()
    {
        // ArtifactRank(enum) 기반 딕셔너리 초기화
        rankedArtifactLists = new Dictionary<ArtifactRank, List<ArtifactSO>>();

        // Enum 값들로 딕셔너리 초기화
        foreach (ArtifactRank rank in System.Enum.GetValues(typeof(ArtifactRank)))
        {
            if (rank != ArtifactRank.None) // None 제외
            {
                rankedArtifactLists[rank] = new List<ArtifactSO>();
            }
        }

        // ArtifactDatas를 ArtifactRank(enum) 기준으로 분류
        foreach (var artifact in artifactDatas)
        {
            ArtifactRank artifactRank = artifact.ArtifactRank.ArtifactRank; // ArtifactData의 ArtifactRank 값 확인
            if (rankedArtifactLists.ContainsKey(artifactRank))
            {
                rankedArtifactLists[artifactRank].Add(artifact);
            }
            else
            {
                Debug.LogWarning($"Unknown rank: {artifactRank} in artifact {artifact.ArtifactName}");
            }
        }
    }

    public ArtifactSO[] GetArtifactDatasRandom(int amount)
    {
        ArtifactSO[] selectedArtifacts = new ArtifactSO[amount];

        // Enum 기반 확률 설정
        Dictionary<ArtifactRank, float> rankRarity = new Dictionary<ArtifactRank, float>();
        float totalRankRarity = 0f;

        foreach (var rankData in artifactRankDatas)
        {
            rankRarity[rankData.ArtifactRank] = rankData.RankRarity; // ArtifactRankDataSO에 저장된 확률
            totalRankRarity += rankData.RankRarity;
        }

        for (int i = 0; i < amount; i++)
        {
            // 1. 첫 번째 랜덤 추출: ArtifactRank 선택
            float randomRankPoint = Random.Range(0f, totalRankRarity);
            float cumulativeRankRarity = 0f;
            ArtifactRank selectedRank = ArtifactRank.None;

            foreach (var rank in rankRarity)
            {
                cumulativeRankRarity += rank.Value;
                if (randomRankPoint <= cumulativeRankRarity)
                {
                    selectedRank = rank.Key;
                    break;
                }
            }


            if (!rankedArtifactLists.ContainsKey(selectedRank) || rankedArtifactLists[selectedRank].Count == 0)
            {
                Debug.LogWarning($"No artifacts available for rank: {selectedRank}");
                continue;
            }

            // 선택된 랭크 리스트에서 랜덤으로 ArtifactData 추출
            List<ArtifactSO> selectedRankList = rankedArtifactLists[selectedRank];
            int artifactIndex = Random.Range(0, selectedRankList.Count);
            ArtifactSO chosenArtifact = selectedRankList[artifactIndex];
            selectedArtifacts[i] = chosenArtifact;

        }

        return selectedArtifacts;
    }
}