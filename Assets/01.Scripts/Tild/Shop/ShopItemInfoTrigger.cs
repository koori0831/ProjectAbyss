using UnityEngine;

public class ShopItemInfoTrigger : MonoBehaviour
{
    public Weapon weapon;
    private Vector2 uiTargetPos;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        uiTargetPos = transform.Find("UITargetPos").GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("¿­¸²");    
        ShopItemInfoManager.Instance.OpenInfo(weapon, uiTargetPos);

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
