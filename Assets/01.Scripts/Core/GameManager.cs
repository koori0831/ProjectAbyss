using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float playTime;
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindAnyObjectByType<Player>();
            }
            return _player;
        }
    }

    private void Update()
    {
        playTime += Time.deltaTime;
    }
}
