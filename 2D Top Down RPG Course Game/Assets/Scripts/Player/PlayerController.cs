using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public bool IsFacingLeft { get { return isFacingLeft; } set { isFacingLeft = value; } }
    private bool isFacingLeft;
    private Vector2 moveInput;
    private PlayerControls playerControls; // PlayerControls adalah class yang di generate sebelumnya jika namanya bukan player control maka berubah juga nama classnya
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        // Inisialisasi player control
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        // Harus Enable dan Disable namun player akan pergi ke semua scene jadi tidak perlu disable
        playerControls.Enable();
    }

    private void Update() 
    {
        PlayerInput();
    }

    private void FixedUpdate() 
    {
        PlayerDirectionFacingAdjustment();
        Move();
    }

    private void PlayerInput()
    {
        // untuk mendapatkan nilai dari input. Movement dan Move adalah nama dari maps framework terkadang bisa berbeda
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * (speed * Time.fixedDeltaTime));
    }

    private void PlayerDirectionFacingAdjustment()
    {
        // Untuk mendapatkan posisi mouse
        Vector3 mousePos = Input.mousePosition;

        // Untuk mengubah koordinat player dari world ke layar
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Untuk membandingkan jika posisi mouse lebih kecil atau berada di kiri maka sprite akan di flip
        if(mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            IsFacingLeft = true;
        }
        else 
        {
            spriteRenderer.flipX = false;
            IsFacingLeft = false;
        }
    }
}
