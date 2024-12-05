using System;
using TMPro;
using UnityEngine;

public class CoinUI : PlayerCunnectUI
{
    [SerializeField] private TextMeshProUGUI _coinText;
    private int _coinAmount;
    public override void AfterFindPlayer()
    {
        _player.GetCompo<PlayerData>()._currentCoin.OnvalueChanged += HandleCoinChange;
        HandleCoinChange(0, _player.GetCompo<PlayerData>()._currentCoin.Value);
    }

    private void OnDestroy()
    {
        _player.GetCompo<PlayerData>()._currentCoin.OnvalueChanged -= HandleCoinChange;
    }

    private void HandleCoinChange(int prev, int next)
    {
        _coinAmount = next;
        _coinText.text = next.ToString();
    }
}
