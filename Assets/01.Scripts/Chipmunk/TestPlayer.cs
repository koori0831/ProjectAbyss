using System;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Rigidbody2D rigidCompo;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    void Awake()
    {
        rigidCompo = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveDir = Input.GetAxis("Horizontal");
        rigidCompo.linearVelocityX = moveDir * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidCompo.linearVelocityY = jumpPower;
        }
    }
}
