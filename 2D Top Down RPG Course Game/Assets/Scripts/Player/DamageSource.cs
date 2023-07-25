using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int playerDamage;
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        
        // mengecek apakah memiliki enemyHealth component, jika iya maka jalankan takedamage
        enemyHealth?.TakeDamage(playerDamage);
    }
}
