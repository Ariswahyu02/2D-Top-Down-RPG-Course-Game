using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    private KnockBack knockBack;
    private int currentHealth;

    private void Awake() {
        knockBack = GetComponent<KnockBack>();
    }

    private void Start() 
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, 15f);
        EnemyDeath();
    }

    private void EnemyDeath()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
