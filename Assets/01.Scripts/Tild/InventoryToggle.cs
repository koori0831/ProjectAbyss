using UnityEngine;
using DG.Tweening;

public class InventoryToggle : MonoBehaviour
{
    private RectTransform _inventoryFrame;

    private bool isOpen;

    private void Awake()
    {
        _inventoryFrame = GetComponent<RectTransform>();   
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Tab) && !isOpen)
        {
           // _inventoryFrame.DOAnchorPos();
        }
    }
}
