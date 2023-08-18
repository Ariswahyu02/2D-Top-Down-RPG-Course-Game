using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    
    private void Update() 
    {
        WeaponFacingToMouse();
    }

    private void WeaponFacingToMouse()
    {
        // agar pedang mengarah pada arah player menghadap
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // agar weapon seperti panah dapat menigkuti mouse 360 derajat 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // flip parent active weapon
        if(mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else 
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void Attack()
    {
        
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
