using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour,IEntityComponent, ISavable
{
    [field: SerializeField] public SaveIDSO IdData { get; private set; }

    private Player _player;
    [SerializeField] private int _currentCoin;
    [SerializeField] private int _killCount;
    
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        //AddCoin을 이벤트에 구독해줘야한다.
    }

    public void AddCoin(int coin)
    {
        _currentCoin += coin;
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
            currentCoin = _currentCoin,
            killCount = _killCount
        };
        return JsonUtility.ToJson(data);
    }

    public void RestoreData(string data)
    {
        PlayerDataSave loadData = JsonUtility.FromJson<PlayerDataSave>(data);
        _currentCoin = loadData.currentCoin;
    }
    #endregion
}