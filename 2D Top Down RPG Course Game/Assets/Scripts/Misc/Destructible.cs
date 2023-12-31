using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject prefabVFX;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<DamageSource>()){
            Instantiate(prefabVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
}
