using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour,IEntityComponent, ISavable
{
    [field: SerializeField] public SaveIDSO IdData { get; private set; }

    private Player _player;
    public NotifyValue<int> _currentCoin = new NotifyValue<int>();
    public int earnCoin { get; private set; }
    [SerializeField] private int _killCount;
    public int KillCount => _killCount;
    [SerializeField] private IntEventChannelSO _killEventChannel;
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _killEventChannel.OnValueEvent+=AddKill;
        _killEventChannel.OnValueEvent+=AddCoin;
    }

    private void OnDestroy()
    {
        _killEventChannel.OnValueEvent-=AddKill;
        _killEventChannel.OnValueEvent-=AddCoin;
    }

    private void AddKill(int obj)
    {
        _killCount += obj;
    }
    private void AddCoin(int coin)
    {
        earnCoin += coin;
        _currentCoin.Value += coin;
    }
    
    #region save system implementation

    [Serializable]  //직렬화 가능하게 해야 JSONUtilty에 의해서 저장된다.
    public struct PlayerDataSave
    {
        public int currentCoin;
        public int killCount;
    }
    
    public string GetSaveData()
    {
        PlayerDataSave data = new PlayerDataSave
        {
            currentCoin = _currentCoin.Value,
            killCount = _killCount
        };
        return JsonUtility.ToJson(data);
    }

    public void RestoreData(string data)
    {
        PlayerDataSave loadData = JsonUtility.FromJson<PlayerDataSave>(data);
        _currentCoin.Value = loadData.currentCoin;
        _killCount= loadData.killCount;
    }
    #endregion
}