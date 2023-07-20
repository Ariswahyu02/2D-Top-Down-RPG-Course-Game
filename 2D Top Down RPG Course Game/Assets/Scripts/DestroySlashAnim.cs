using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySlashAnim : MonoBehaviour
{
   public void SelfDestroy()
   {
     Destroy(gameObject);
   }
}
