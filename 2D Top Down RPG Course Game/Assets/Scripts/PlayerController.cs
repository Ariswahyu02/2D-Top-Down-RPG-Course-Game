using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 moveInput;
    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private void Awake() 
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    private void Update() 
    {
        PlayerInput();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void PlayerInput()
    {
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * (speed * Time.fixedDeltaTime));
    }
}
