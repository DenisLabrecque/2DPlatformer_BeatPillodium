using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// Blue Fireball attack prefab shot by the player and damaging enemies;
// it requires that the player be tagged "Player", and must be assigned
// to the player's GameObject slot. Enemies must be tagged "Enemy".

public class BlueFireball : MonoBehaviour {
   private float speed        = 12.5f;
   private int   damageCaused = 6; // Damage this attack causes enemies
   private int   direction    = 1; // 1 or -1 (right or left shot)

   void Start ()
   {
      // A fireball won't reach the other end of the map
      Destroy(gameObject, 7.0f);
   }

   void Update ()
   {
      transform.Translate(speed * direction * Time.deltaTime, 0, 0);
   }

   // Called by the player which instantiates the fireball
   public void setDirection(int multiplier)
   {
      // Right is 1, left is -1; error check
      if (multiplier > 0)
      {
         // Direction is preset to 1
      }
      else
      {
         direction = -1; // Go left
      }
   }

   // Damage enemies when they get hit
   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.tag == "Enemy")
      {
         collision.gameObject.SendMessage("damage", damageCaused);
         Destroy(gameObject);
      }
   }
}
