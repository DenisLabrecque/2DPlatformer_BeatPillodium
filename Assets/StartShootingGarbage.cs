using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script shoots garbage to attack the player. It requires
// that the player be tagged "Player", and must be attached to
// a child box collider of the Krubbish asset.

public class StartShootingGarbage : MonoBehaviour {
   private KrubbishAI krubbishAI;

   void Start ()
   {
      krubbishAI = GetComponentInParent<KrubbishAI>();
   }

   // Start the Krubbish and make him shoot tin cans
   private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.tag == "Player")
      {
         krubbishAI.awaken();
         krubbishAI.shootGarbage(true);
      }
   }

   // Stop shooting when the player is out of the killzone;
   // continue going forwards
   private void OnTriggerExit2D(Collider2D collision) {
      if (collision.gameObject.tag == "Player")
      {
         krubbishAI.shootGarbage(false);
      }
   }
}
