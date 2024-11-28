using UnityEngine;

public class ShopItemInfoTrigger : MonoBehaviour
{
    public ArtifactData artifact;
    private Vector2 uiTargetPos;
    public SpriteRenderer itemSprite;

    private void Awake()
    {
        artifact = GetComponentInChildren<ArtifactData>();
        uiTargetPos = transform.Find("UITargetPos").GetComponent<Transform>().position;
        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("¿­¸²");    
        ShopItemInfoManager.Instance.OpenInfo(artifact, uiTargetPos);

        LayerMask collisionLayerMask = 1 << collision.gameObject.layer;
        Debug.Log(collisionLayerMask.value);
        // if ((collisionLayerMask & ) != 0)
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShopItemInfoManager.Instance.CloseInfo();
        LayerMask collisionLayerMask = 1 << collision.gameObject.layer;
       // if ((collisionLayerMask & climbingLayerMask) != 0)
            
    }

    


}
