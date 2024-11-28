using System.Collections;
using UnityEngine;

public class ZipLineBullet : MonoBehaviour, IPoolable, IInterationable
{
    [SerializeField] private string _poolName = "ZipLineBullet";

    private Rigidbody2D _rigidBody;

    public string PoolName => _poolName;

    public GameObject ObjectPrefab => gameObject;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
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

    }
}
