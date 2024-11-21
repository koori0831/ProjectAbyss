using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopItemInfoManager : MonoSingleton<ShopItemInfoManager>
{

    private CanvasGroup _infoFrame;
    private RectTransform _infoFramePos;
    private CanvasGroup _decoFrame;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemRank;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _sellPrice;
    [SerializeField] private TMP_Text _itemDesc;
    [SerializeField] private Image _rankDeco;
    

    private bool _isOpen;

    private void Awake()
    {
        _infoFrame = GetComponent<CanvasGroup>();
        _infoFramePos = GetComponent<RectTransform>();
        _decoFrame = _rankDeco.GetComponentInChildren<CanvasGroup>();
    }

    public void OpenInfo(Weapon weapon, Vector2 uiTargetPos)
    {
        

        Debug.Log("¿­¸²");
        _infoFrame.DOFade(1, 0.5f);
        _decoFrame.DOFade(1, 0.1f).SetDelay(0.1f);
        _infoFramePos.position = uiTargetPos;
            
        InitializeUI(weapon);

        

    }

    public void InitializeUI(Weapon weapon)
    {
        _itemName.SetText(weapon.ArtifactName);
        _itemDesc.SetText(weapon.ArtifactDesc);
        _price.SetText("$"+ weapon.SaleValue.ToString());
        _sellPrice.SetText("$" + weapon.ResaleValue.ToString());

        

    }

    public void CloseInfo()
    {
        _decoFrame.DOFade(0, 0.1f);
        _infoFrame.DOFade(0, 0.2f).SetDelay(0.1f);
    }



}
