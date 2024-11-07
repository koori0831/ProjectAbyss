using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCunnectUI : MonoBehaviour
{
    protected Player _player;

    protected virtual void Start()
    {
        StartCoroutine(FindPlayerCoroutine());
    }

    private IEnumerator FindPlayerCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance.Player != null);
        _player = GameManager.Instance.Player;
        AfterFindPlayer();
    }

    public abstract void AfterFindPlayer();
}
