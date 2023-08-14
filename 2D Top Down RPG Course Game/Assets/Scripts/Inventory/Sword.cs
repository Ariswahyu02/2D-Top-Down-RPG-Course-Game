using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    private GameObject slashAnim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private Animator animator;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        WeaponFacingToMouse();
    }

    public void Attack()
    {
        //isAttacking = true;
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
        
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(.5f);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
        // isAttacking = false;
    }
    

    public void OnDoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void FlipUpSlashAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if(playerController.IsFacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void FlipDownSlashAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(playerController.IsFacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
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
            weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
