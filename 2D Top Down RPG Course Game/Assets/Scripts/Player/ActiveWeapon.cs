using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon {get; private set;}
    private PlayerControls playerControls;
    private float weaponCooldown;
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        // action untuk klik kiri dan mengeksekusi attack saat klik kiri
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        // Biar ga langsung nyerang waktu awal game
        AttackCD();
    }

    private void Update() {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCD();
        weaponCooldown = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown; 
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCD()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(weaponCooldown);
        isAttacking = false;
    }
    
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void Attack()
    {
        if(attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCD();
            (CurrentActiveWeapon as IWeapon).Attack(); 
        }
    }
}
