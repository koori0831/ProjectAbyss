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
    [SerializeField] private Image _rankDeco2;

    private bool _isOpen;

    private void Awake()
    {
        _infoFrame = GetComponent<CanvasGroup>();
        _infoFramePos = GetComponent<RectTransform>();
        _decoFrame = _rankDeco.GetComponentInChildren<CanvasGroup>();
    }

    public void OpenInfo(ArtifactSO artifact, Vector2 uiTargetPos)
    {


        Debug.Log("¿­¸²");
        _infoFrame.DOFade(1, 0.5f);
        _decoFrame.DOFade(1, 0.1f).SetDelay(0.1f);
        _infoFramePos.position = uiTargetPos;

        InitializeUI(artifact);



    }

    public void InitializeUI(ArtifactSO artifact)
    {
        _itemName.SetText(artifact.ArtifactName);
        _itemDesc.SetText(artifact.ArtifactDesc);

        _price.SetText("$" + artifact.SaleValue.ToString());
        _sellPrice.SetText("$" + artifact.ResaleValue.ToString());

        _itemRank.SetText(artifact.ArtifactRank.RankName);
        _itemRank.DOColor(artifact.ArtifactRank.RankColor, 0.1f);
        _rankDeco.DOColor(artifact.ArtifactRank.RankColor, 0.1f);
        _rankDeco2.DOColor(artifact.ArtifactRank.RankColor, 0.1f);
    }

    public void CloseInfo()
    {
        _decoFrame.DOFade(0, 0.1f);
        _infoFrame.DOFade(0, 0.2f).SetDelay(0.1f);

        _itemRank.DOColor(new Color32(255, 255, 255, 255), 0.1f);

        _rankDeco.DOColor(new Color32(255, 255, 255, 255), 0.1f);
        _rankDeco2.DOColor(new Color32(255, 255, 255, 255), 0.1f);
    }



}