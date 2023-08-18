using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 22f;
    [SerializeField] private GameObject projectileParticleOnDeath;
    private WeaponInfo weaponInfo;
    private Vector2 startPos;

    private void Start() {
        startPos = transform.position;
    }

    private void Update() 
    {
        MoveProjectile();    
        DetectFireDistance();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void DetectFireDistance()
    {
        if(Vector2.Distance(transform.position, startPos) > weaponInfo.weaponRange)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
       EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
       Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

       if(!other.isTrigger && (enemyHealth || indestructible))
       {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(projectileParticleOnDeath, transform.position, transform.rotation);
            Destroy(gameObject);
       }

    }

}