using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteTransparency : MonoBehaviour
{
   private SpriteRenderer spriteRenderer;
   private Animator animator;
   private Tilemap tilemap;

   private void Awake()
   {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        tilemap = GetComponent<Tilemap>();
   }

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.GetComponent<PlayerController>()){
      if(spriteRenderer != null){
        animator.SetBool("spriteFade", true); 
        }else if(tilemap != null){
          animator.SetBool("tilemapFade", true);
        }
      }
   }

   private void OnTriggerExit2D(Collider2D other) {
    if(other.gameObject.GetComponent<PlayerController>())
      {   
        if(spriteRenderer != null){
        animator.SetBool("spriteFade", false); 
        }else if(tilemap != null){
          animator.SetBool("tilemapFade", false);
        }
      }
   }
}
