using System;
using UnityEngine;
using Chipmunk.Library;

public class EntityMover : MonoBehaviour, IEntityComponent
{
    [Header("Move values")]
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private Transform _groundTrm;
    public LayerMask _whatIsGround;
    [SerializeField] private Vector2 _groundCheckSize;


    public NotifyValue<bool> isGround = new();
    public Vector2 Velocity => _rbCompo.linearVelocity;

    public float SpeedMultiplier { get; set; } = 1f;
    public bool CanManualMove { get; set; } = true;

    private float _originalGravityScale;

    private Entity _entity;
    private EntityRenderer _renderer;
    private Rigidbody2D _rbCompo;

    private float _xMovement; //????? ?��???? ????

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _renderer = _entity.GetCompo<EntityRenderer>();
        _rbCompo = _entity.GetComponent<Rigidbody2D>();

        _originalGravityScale = _rbCompo.gravityScale;
    }

    public void SetGravityScale(float value) 
            => _rbCompo.gravityScale = _originalGravityScale * value;

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
        bool before =isGround.Value ;
        isGround.Value = Physics2D.OverlapBox(
            _groundTrm.position, _groundCheckSize, 0, _whatIsGround);
    }

    private void MoveCharacter()
    {
        if (CanManualMove)
        {
            _rbCompo.linearVelocityX = _xMovement * _moveSpeed * SpeedMultiplier;
            _renderer.FlipController(_xMovement);
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundTrm == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundTrm.position, _groundCheckSize);
    }


}
