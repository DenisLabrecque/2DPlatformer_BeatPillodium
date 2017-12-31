using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque

public class AwakenPillodium : MonoBehaviour {

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.tag == "Player")
      {
         GetComponentInParent<PillodiumAI>().awakenPillodium();
      }
   }
}
