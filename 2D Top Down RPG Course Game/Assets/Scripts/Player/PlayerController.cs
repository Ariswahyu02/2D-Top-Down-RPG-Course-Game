using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private Transform slashAnimPoint;
    public bool IsFacingLeft { get { return isFacingLeft; } set { isFacingLeft = value; } }
    private bool isDashing = false;
    private bool isFacingLeft;
    private float startingSpeed;
    private Vector2 moveInput;
    private PlayerControls playerControls; // PlayerControls adalah class yang di generate sebelumnya jika namanya bukan player control maka berubah juga nama classnya
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override void Awake() 
    {
        base.Awake();
        // Inisialisasi player control
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() 
    {
        playerControls.Combat.Dash.performed += _ => Dash();    
        startingSpeed = speed;
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

    public Transform GetWeaponColliderTransform()
    {
        return weaponCollider;
    }
    
    public Transform GetSlashAnimTransform()
    {
        return slashAnimPoint;
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

    private void Dash()
    {
        if(!isDashing)
        {
            isDashing = true;
            speed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        // dash duration
        yield return new WaitForSeconds(.2f);
        speed = startingSpeed;
        trailRenderer.emitting = false;

        // dash cooldown
        yield return new WaitForSeconds(.3f);
        isDashing = false;
    }
}
