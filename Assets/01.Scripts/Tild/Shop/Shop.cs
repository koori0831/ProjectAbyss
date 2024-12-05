using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ArtifactSO artifact;
    private Vector2 uiTargetPos;
    public SpriteRenderer itemSprite;
    private bool isJoined;

    private bool isOnCooldown;
    [SerializeField] private float cooldown = 1.5f; // 쿨타임 설정 (초)

    private void Awake()
    {
        uiTargetPos = transform.Find("UITargetPos").GetComponent<Transform>().position;
        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isJoined = true;
        ShopItemInfoManager.Instance.OpenInfo(artifact, uiTargetPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isJoined = false;
        ShopItemInfoManager.Instance.CloseInfo();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isJoined && !isOnCooldown)
        {
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        isOnCooldown = true; // 쿨타임 시작
        if (MoneySample.Instance.Money >= artifact.SaleValue)
        {
            MoneySample.Instance.ChangeMoney(-artifact.SaleValue);
            ChatManager.Instance.StartChat($"${artifact.SaleValue}으로 {artifact.ArtifactName}을(를) 구매했습니다.", 2f);
        }
        else
        {
            ChatManager.Instance.StartChat($"{artifact.ArtifactName}을(를) 구매하려면 ${MoneySample.Instance.Money - artifact.SaleValue}가 더 필요합니다.", 3f);
        }
        yield return new WaitForSeconds(cooldown); // 쿨타임 대기
        isOnCooldown = false; // 쿨타임 해제
    }
}
