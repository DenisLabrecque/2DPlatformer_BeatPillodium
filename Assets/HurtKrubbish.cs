using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script performs an attack to the Krubbish when the player jumps on him.
// For this script to work, it must be part of a child collider of a game object
// with the KrubbishAI script, and the player must be tagged "Player".

public class HurtKrubbish : MonoBehaviour {

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if(collision.gameObject.tag == "Player")
      {
         GetComponentInParent<KrubbishAI>().damage(7);
      }
   }
}
