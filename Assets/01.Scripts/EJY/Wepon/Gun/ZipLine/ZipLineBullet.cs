using System;
using System.Collections;
using UnityEngine;

public class ZipLineBullet : MonoBehaviour, IPoolable, IInterationable
{
    [SerializeField] private string _poolName = "ZipLineBullet";

    [SerializeField] private float _lifeTime = 15;
    private float _currentLifeTime = 0;
    private bool _isPlaced = false;

    public ZipLineBullet linkedBullet;

    private Rigidbody2D _rigidBody;

    public string PoolName => _poolName;
    [SerializeField] Material zipLineMat;

    public GameObject ObjectPrefab => gameObject;
    public bool isLinked = false;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;

        if (_currentLifeTime >= _lifeTime)
        {
            //_isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isPlaced) return;

        _isPlaced = true;
        _rigidBody.linearVelocity = Vector2.zero;

        TryLink();
        Debug.Log(1);
    }

    private void TryLink()
    {
        if (linkedBullet != null)
        {
            if (linkedBullet._isPlaced)
            {
                GameObject gameObject = new GameObject("Line");
                ZipLine zipLine = gameObject.AddComponent<ZipLine>();
                zipLine.Initialize(zipLineMat);
                zipLine.Link(this, linkedBullet);
            }
        }
    }

    public void Interaction()
    {
        StartCoroutine(ZipLineInteractionCoroutine());
    }

    private IEnumerator ZipLineInteractionCoroutine()
    {
        yield return null;
    }

    public void Fire(Vector3 firePos, Vector3 velocity)
    {
        transform.position = firePos;
        transform.right = velocity.normalized;
        _rigidBody.linearVelocity = velocity;
    }

    public Vector3 ReturnBulletPosition()
    {
        return transform.position;
    }

    public void ResetItem()
    {
        _isPlaced = false;
    }
}
