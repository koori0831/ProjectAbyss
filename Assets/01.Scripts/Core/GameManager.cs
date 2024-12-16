using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            Player.GetCompo<EntityHealth>().ApplyDamage(10000,Vector2.zero, Vector2.zero, Player);
        }
    }
}
