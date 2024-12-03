using System.Collections;
using UnityEngine;

public class ZipLineBullet : MonoBehaviour, IPoolable, IInteractionable
{
    private Player _player;
    private ZipLine _zipLine;

    [SerializeField] private string _poolName = "ZipLineBullet";

    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float _placeLifeTime = 20;
    [SerializeField] private float _actLifeTime = 30;

    private float LifeTime => _isPlaced ? isLinked ? _actLifeTime : _placeLifeTime : 15;

    public float currentLifeTime = 0;

    private bool _isPlaced = false;

    public ZipLineBullet linkedBullet;
    public bool isLinked = false;

    private Rigidbody2D _rigidBody;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _zipLine = FindAnyObjectByType<ZipLine>().GetComponent<ZipLine>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log(LifeTime);

        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= LifeTime)
        {
            PoolManager.Instance.Push(this);
            _zipLine.ResetLineRenderer();
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isPlaced) return;

        _isPlaced = true;
        currentLifeTime = 0;

        _rigidBody.linearVelocity = Vector2.zero;

        TryLink();
    }

    private void TryLink()
    {
        if (linkedBullet != null)
        {
            if (linkedBullet._isPlaced)
            {
                _zipLine.Link(this, linkedBullet);
            }
        }
    }

    public void Interaction()
    {
        StartCoroutine(ZipLineInteractionCoroutine());
    }

    private IEnumerator ZipLineInteractionCoroutine()
    {
        float time = 0;

        Vector2 startPos = transform.position;
        Vector2 endPos = linkedBullet.transform.position;

        float dis = Vector2.Distance(startPos, endPos);

        float moveTime = dis / _moveSpeed;

        _player.StateMachine.ChangeState(PlayerStateEnum.PlayerZipLine);

        while (time < moveTime)
        {
            if (linkedBullet == null) break;

            time += Time.deltaTime;

            _player.transform.position = Vector2.Lerp(startPos, endPos, time / moveTime);
            yield return null;
        }
        _player.StateMachine.ChangeState(PlayerStateEnum.PlayerIdle);
    }

    public void Fire(Transform firePos, float velocity)
    {
        transform.SetPositionAndRotation(firePos.position, firePos.rotation);
        _rigidBody.linearVelocity = firePos.right * _player.transform.localScale.x * velocity;
    }

    public void ResetItem()
    {
        _isPlaced = false;
        isLinked = false;
    }
}
