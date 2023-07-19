using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private Animator animator;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        // action untuk klik kiri dan mengeksekusi attack saat klik kiri
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        WeaponFacingToMouse();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void WeaponFacingToMouse()
    {
        // agar pedang mengarah pada arah player menghadap
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        // agar weapon seperti panah dapat menigkuti mouse 360 derajat 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // flip parent active weapon
        if(mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
