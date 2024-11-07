using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    PlayerIdle,
    PlayerMove,
    PlayerZipLine,
    PlayerInteraction,
    PlayerAttack
}

public class Player : Entity
{
   public StateMachine<PlayerState> StateMachine { get; private set; }
}