using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private GameObject deathVFXPrefab;
    private HitFlash hitFlash;
    private KnockBack knockBack;
    private int currentHealth;

    private void Awake() {
        knockBack = GetComponent<KnockBack>();
        hitFlash = GetComponent<HitFlash>();
    }

    private void Start() 
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(hitFlash.FlashRoutine());
        StartCoroutine(CheckDeathCoroutine());
    }

    private IEnumerator CheckDeathCoroutine()
    {
        yield return new WaitForSeconds(hitFlash.GetFlashTime());
        EnemyDeath();
    }

    private void EnemyDeath()
    {
        if(currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
