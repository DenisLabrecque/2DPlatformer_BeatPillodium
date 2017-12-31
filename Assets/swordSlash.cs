using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Limantara
// Attached tot he child of Knight gameObjet that has trigger collider component.
// Deal damage to enemies with "Enemy" tag.

public class swordSlash : MonoBehaviour {
   private int slashDamage = 9;

   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.tag == "Enemy")
      {
         other.gameObject.SendMessage("damage", slashDamage);
      }
   }
}
