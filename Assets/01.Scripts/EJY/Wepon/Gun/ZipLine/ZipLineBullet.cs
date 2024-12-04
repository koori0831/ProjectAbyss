using System.Collections;
using UnityEngine;

public class ZipLineBullet : MonoBehaviour, IPoolable, IInteractionable
{
    private Player _player;
    private ZipLine _zipLine;
    private ZipLineGun _zipLineGun;
    private Rigidbody2D _rigidBody;

    public ZipLineBullet linkedBullet;

    [SerializeField] private float _moveSpeed = 5;

    [SerializeField] private float _placeLifeTime = 20;
    [SerializeField] private float _actLifeTime = 30;

    private float LifeTime => _isPlaced ? isLinked ? _actLifeTime : _placeLifeTime : 15;

    public float currentLifeTime = 0;

    [HideInInspector]
    public bool _isPlaced = false;
    [HideInInspector]
    public bool isLinked = false;


    public string PoolName => _poolName;
    [SerializeField] private string _poolName = "ZipLineBullet";
    public GameObject ObjectPrefab => gameObject;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _zipLine = FindAnyObjectByType<ZipLine>();
        _zipLineGun = FindAnyObjectByType<ZipLineGun>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= LifeTime)
        {
            PoolManager.Instance.Push(this);
            if (isLinked)
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
        if (_zipLineGun.beforeShootedBullet == null)
        {
            _zipLineGun.beforeShootedBullet = this;
        }
        else
        {
            SetLinkBullet();
        }
    }

    public void SetLinkBullet()
    {
        _zipLineGun.LinkBullet(this, _zipLineGun.beforeShootedBullet);
        _zipLine.Link(this, linkedBullet);
        _zipLineGun.ResetBefore();
    }

    public void Interaction()
    {
        StartCoroutine(ZipLineInteractionCoroutine());
    }

    private IEnumerator ZipLineInteractionCoroutine()
    {
        if (linkedBullet != null)
        {
            float time = 0;

            Vector2 startPos = transform.position;
            Vector2 endPos = linkedBullet.transform.position;

            float dis = Vector2.Distance(startPos, endPos);

            //float offset = new Vector2(); 

            float moveTime = dis / _moveSpeed;

            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerZipLine);
            _player.canFire = false;

            while (time < moveTime)
            {
                if (linkedBullet == null) break;

                time += Time.deltaTime;

                _player.transform.position = Vector2.Lerp(startPos, endPos, time / moveTime);
                yield return null;
            }
            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerIdle);
            _player.canFire = true;
        }
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
        linkedBullet = null;
        currentLifeTime = 0;
    }
}
