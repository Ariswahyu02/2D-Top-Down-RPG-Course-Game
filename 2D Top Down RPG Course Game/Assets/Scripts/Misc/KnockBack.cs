using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool isGettingKnocked {get; private set;}
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void GetKnockBack(Transform attackerPos, float knockThrust)
    {
        isGettingKnocked = true;

        // Buat mendapatkan nilai baru dari si knocker dan implement ke rb.AddForce
        // Agaar bisa knock back Pergerakan RigidBody di EnemyPathFinding harus dihentikan
        Vector2 difference = (transform.position - attackerPos.position).normalized * knockThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);

        StartCoroutine(KnockBackRoutine());
        
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(.2f);
        rb.velocity = Vector2.zero;
        isGettingKnocked = false;
    }
}
