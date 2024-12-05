using UnityEngine;

public class Shop : MonoBehaviour
{
    public ArtifactData artifact;
    private Vector2 uiTargetPos;
    public SpriteRenderer itemSprite;

    private bool isJoined;

    private void Awake()
    {
        artifact = GetComponentInChildren<ArtifactData>();
        uiTargetPos = transform.Find("UITargetPos").GetComponent<Transform>().position;
        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isJoined = true;
        Debug.Log("열림");    
        ShopItemInfoManager.Instance.OpenInfo(artifact, uiTargetPos);

        LayerMask collisionLayerMask = 1 << collision.gameObject.layer;
        Debug.Log(collisionLayerMask.value);
        // if ((collisionLayerMask & ) != 0)
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isJoined = false;
        ShopItemInfoManager.Instance.CloseInfo();
        LayerMask collisionLayerMask = 1 << collision.gameObject.layer;
       // if ((collisionLayerMask & climbingLayerMask) != 0)
            
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isJoined)
        {
            
            if (MoneySample.Instance.Money >= artifact.SaleValue)
            {
                MoneySample.Instance.ChangeMoney(-artifact.SaleValue);
                ChatManager.Instance.StartChat($"${artifact.SaleValue}으로 {artifact.ArtifactName}을(를) 구매했습니다.",2f);
            }
            else
            {   
                ChatManager.Instance.StartChat($"{artifact.ArtifactName}을(를) 구매하려면 ${MoneySample.Instance.Money - artifact.SaleValue}가 더 필요합니다. ",3f);
            }
        }
    }




}
