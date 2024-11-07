using DG.Tweening;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected ItemSO _itemData;
    [SerializeField] protected float _dropDelay = 0.2f;

    protected bool _alreadyCollected;
    protected Rigidbody2D _rbCompo;
    protected Collider2D _colliderCompo;

    protected bool _canCollectable;

    protected SpriteRenderer _spriteRenderer;

    protected virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
        _colliderCompo = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItemData(ItemSO itemData)
    {
        _itemData = itemData;
        if (_itemData.itemType == ItemType.Ammo)
        {
            _spriteRenderer.sprite = _itemData.itemSprite;
        }
    }
    public void DropIt(Vector3 position, Vector2 force)
    {
        transform.position = position;
        _rbCompo.AddForce(force, ForceMode2D.Impulse);
        DOVirtual.DelayedCall(_dropDelay, () => _canCollectable = true);
    }

    public abstract void Collect(Transform collector, float magneticPower);
}
