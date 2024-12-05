using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText, _earnText, _killText, _depthText;
    [SerializeField] private DepthUI _depthUI;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        GameManager.Instance.Player.GetCompo<EntityHealth>().OnDeathEvent += ShowResultUI;
    }
    
    private void ShowResultUI()
    {
        string formattedTime = TimeSpan.FromSeconds(GameManager.Instance.playTime).ToString(@"hh\:mm\:ss");
        _timeText.text = $"TIME : {formattedTime}";

        PlayerData data = GameManager.Instance.Player.GetCompo<PlayerData>();
        _earnText.text = $"EARN : {data.earnCoin}";
        _killText.text = $"KILL : {data.KillCount}";
        _depthText.text = $"Depth : {_depthUI.Depth}m";
        
        _panel.SetActive(true);
    }
    
    public void RestartGame()
    {
        //상점으로 ㄱㄱ
    }
    public void GoTitle()
    {
        //타이틀로 ㄱㄱ
        SceneManager.LoadScene("Title");
    }
}
