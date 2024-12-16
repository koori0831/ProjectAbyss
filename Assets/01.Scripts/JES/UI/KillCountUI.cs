using System;
using TMPro;
using UnityEngine;

public class KillCountUI : PlayerCunnectUI
{
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private IntEventChannelSO _killEventChannel;
    private int _killCount=0;
    public override void AfterFindPlayer()
    {
        _killEventChannel.OnValueEvent += AddKillCount;
    }

    private void OnDestroy()
    {
        _killEventChannel.OnValueEvent -= AddKillCount;
    }

    private void AddKillCount(int obj)
    {
        _killCount = obj;
        _killCountText.text = _killCount.ToString();
    }
}
