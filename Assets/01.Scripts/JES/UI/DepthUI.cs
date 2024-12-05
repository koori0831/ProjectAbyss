using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepthUI : PlayerCunnectUI, ISavable
{
    [SerializeField] private Image _depthBar;
    [SerializeField] private TextMeshProUGUI _depthText;
    [SerializeField] private float _startY=0, _endY=-1000;
    private Transform _playerTrm;
    public override void AfterFindPlayer()
    {
        _playerTrm = _player.transform;
        _depthBar.fillAmount = 0;
        _depthText.text = "0m";
    }

    private void Update()
    {
        if(_playerTrm==null) return;
        DOTween.CompleteAll();
        
        
        float yPos = _playerTrm.position.y;

        if (yPos > _startY)
        {
            _depthBar.fillAmount = 0;
            _depthText.text = "0m";
            return;
        }
        yPos = Mathf.RoundToInt(Mathf.Abs(yPos));
        _depthText.text = $"{yPos}m";
        yPos /= Mathf.Abs(_endY);
        _depthBar.DOFillAmount(yPos, 0.1f);
    }

    #region Saving

    public SaveIDSO IdData { get; }
    public string GetSaveData()
    {
        throw new NotImplementedException();
    }

    public void RestoreData(string data)
    {
        throw new NotImplementedException();
    }

    #endregion
    
}
