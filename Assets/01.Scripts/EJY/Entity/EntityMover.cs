using System;
using UnityEngine;
using Chipmunk.Library;

public class EntityMover : MonoBehaviour, IEntityComponent
{
    [Header("MoveCharacter values")]
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private Transform _groundTrm;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector2 _groundCheckSize;


    public event Action<bool> OnGroundStateChange;

    public NotifyValue<bool> isGround = new();

    private Entity _entity;
    private EntityRenderer _renderer;
    private Rigidbody2D _rbCompo;

    private float _xMovement;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _renderer = _entity.GetCompo<EntityRenderer>();
        _rbCompo = GetComponentInParent<Rigidbody2D>();
    }

    public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
    {
        _rbCompo.AddForce(force, mode);
    }

    public void StopImmediately(bool isYAxisToo = false)
    {
        if (isYAxisToo)
            _rbCompo.linearVelocity = Vector2.zero;
        else
            _rbCompo.linearVelocityX = 0;
        _xMovement = 0;
    }

    public void SetXMovement(float xMovement)
    {
        _xMovement = xMovement;
    }

    private void FixedUpdate()
    {
        CheckGround();
        MoveCharacter();
    }

    private void CheckGround()
    {
        isGround.Value = Physics2D.OverlapBox(_groundTrm.position, _groundCheckSize, 0, _whatIsGround);
    }

    private void MoveCharacter()
    {
        _rbCompo.linearVelocityX = _xMovement * _moveSpeed;
        _renderer.FlipController(_xMovement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_groundTrm.position, _groundCheckSize);
    }


}
