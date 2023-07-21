using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }
    private void FixedUpdate() {
        if(knockBack.isGettingKnocked) { return; }
        
        rb.MovePosition(rb.position + moveDir * (speed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 target)
    {
        moveDir = target;
    }
}
