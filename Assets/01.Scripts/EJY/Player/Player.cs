using System;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.ZipLineSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStateEnum
{
    PlayerIdle,
    PlayerMove,
    PlayerJump,
    PlayerFall,
    PlayerZipLine,
    PlayerAttack
}

public class Player : Entity, IZipLineRideable
{
    public Action OnAttackEvent;

    [field: SerializeField]
    public PlayerInputSO InputCompo { get; private set; }
    public StateMachine<PlayerStateEnum> StateMachine { get; private set; }

    private Dictionary<Type, IPlayerComponent> _playerComponents = new Dictionary<Type, IPlayerComponent>();

    public EntityMover MoveCompo { get; private set; }
    public EntityRenderer RenderCompo { get; private set; }

    public bool canAttack;
    public bool canFlip;

    [field: Header("Interaction Catch")]

    [field: SerializeField]
    public bool CanInteraction { get; private set; } = false;

    [field: SerializeField] public Rigidbody2D rigidCompo { get; private set; }
    public ZipLineRope connectingRope { get; set; }

    [SerializeField] private Transform _interactionCheckTrm;
    [SerializeField] private Vector2 _interactionCheckSize;
    [SerializeField] private LayerMask _whatIsInteraction;

    [Header("Jump Info")]
    public float jumpPower;

    protected override void Awake()
    {
        base.Awake();

        GetComponentsInChildren<IPlayerComponent>(true).ToList()
            .ForEach(component => _playerComponents.Add(component.GetType(), component));

        _playerComponents.Add(InputCompo.GetType(), InputCompo);

        InitPlayerCompo();

        StateMachine = new StateMachine<PlayerStateEnum>(this);
        StateMachine.InitState(PlayerStateEnum.PlayerIdle);

        canAttack = true;
        canFlip = true;

    }

    private void Update()
    {
        PlayerFlip();
        StateMachine.StateUpdate();
    }

    private void PlayerFlip()
    {
        if (canFlip)
        {
            RenderCompo.FlipController(Mathf.Sign(InputCompo.MousePos.x - transform.position.x));
        }
    }

    private void FixedUpdate()
    {

        MoveCompo.MoveCharacter(true);

        StateMachine.StateFixedUpdate();
    }

    private void OnDisable()
    {
        InputCompo.InteractionEvent -= Interaction;
    }

    private void Interaction()
    {
        IZipLineRideable rideable = this as IZipLineRideable;
        rideable.TryRideRope();
        
        Collider2D interactionObj = Physics2D.OverlapBox(_interactionCheckTrm.position, _interactionCheckSize, 0, _whatIsInteraction);

        CanInteraction = interactionObj;

        if (interactionObj == null) return;

        if (interactionObj.TryGetComponent(out IInteractionable interaction))
        {
            interaction.Interaction();
        }

    }

    protected override void AfterInit()
    {
        base.AfterInit();

        MoveCompo = GetCompo<EntityMover>();
        RenderCompo = GetCompo<EntityRenderer>();

        InputCompo.InteractionEvent += Interaction;
    }

    private void InitPlayerCompo()
    {
        _playerComponents.Values.ToList().ForEach(component => component.Initialize(this));
    }

    public T GetPlayerCompo<T>(bool isDerived = false) where T : IPlayerComponent
    {
        if (_playerComponents.TryGetValue(typeof(T), out var component))
        {
            return (T)component;
        }

        if (isDerived != false)
        {
            Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];
        }

        return default;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_interactionCheckTrm.position, _interactionCheckSize);
    }

    public void OnRide(ZipLineRope rope)
    {
    }

}