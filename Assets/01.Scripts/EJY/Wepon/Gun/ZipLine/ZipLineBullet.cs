using System.Collections;
using UnityEngine;

public class ZipLineBullet : MonoBehaviour, IPoolable, IInteractionable
{
    private Player _player;
    private ZipLineGun _zipLineGun;
    private Rigidbody2D _rigidBody;

    private Vector2 startPos;
    private Vector2 _endPos;

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
                ZipLineManager.Instance.ResetLineRenderer();
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
            // 얘가 첫빠따
            _zipLineGun.beforeShootedBullet = this;
        }
        else
        {
            // 두번째가 실행해줌
            SetLinkBullet();
        }
    }

    public void SetLinkBullet()
    {
        // 서로 링크
        ZipLineManager.Instance.LinkBullet(this, _zipLineGun.beforeShootedBullet);

        SetLinkPos();
        linkedBullet.SetLinkPos();

        if (ZipLineManager.Instance.CheckPathBetweenBullets(startPos, linkedBullet.startPos))
        {
            Debug.Log("두 탄환 사이에 뭔가 있음");
            ZipLineManager.Instance.UnlinkBullet(this, _zipLineGun.beforeShootedBullet);
            _zipLineGun.ResetBefore(this);
            Debug.Log(_zipLineGun.beforeShootedBullet);
            return;
        }
        Debug.Log("두 탄환 사이에 뭔가 없음");

        ZipLineManager.Instance.Link(this, linkedBullet);
        _zipLineGun.ResetBefore();
    }

    public void SetLinkPos()
    {
        startPos = transform.position;
        _endPos = linkedBullet.transform.position;
    }

    public void Interaction()
    {
        StartCoroutine(ZipLineInteractionCoroutine());
    }

    private IEnumerator ZipLineInteractionCoroutine()
    {
        if (linkedBullet != null)
        {
            BoxCollider2D playerCollider = _player.GetComponent<BoxCollider2D>();

            float time = 0;

            /*float offsetX = playerCollider.size.x / 2;
            float offsetY = playerCollider.size.y / 2;

            if (_startPos.x > _endPos.x)
            {
                _startPos.x -= offsetX;
                _endPos.x += offsetX;
            }
            else
            {
                _startPos.x += offsetX;
                _endPos.x -= offsetX;
            }*/

            float dis = Vector2.Distance(startPos, _endPos);

            float moveTime = dis / _moveSpeed;

            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerZipLine);

            while (time < moveTime)
            {
                if (linkedBullet == null) break;

                time += Time.deltaTime;

                _player.transform.position = Vector2.Lerp(startPos, _endPos, time / moveTime);
                yield return null;
            }

            _player.StateMachine.ChangeState(PlayerStateEnum.PlayerIdle);
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
