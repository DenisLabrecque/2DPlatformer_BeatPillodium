using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// Boss attack of the wings hitting the player.

public class WingAttack : MonoBehaviour
{

   private GameObject player;
   private knightControl knightControl;
   private int damageCaused = 6;

   // Use this for initialization
   void Start()
   {
      player        = GameObject.FindGameObjectWithTag("Player");
      knightControl = player.GetComponent<knightControl>();
   }

   void OnCollisionEnter2D(Collision2D collision)
   {

      // Hurt the player
      if (collision.gameObject.tag == "Player")
      {
         knightControl.attackPlayer(damageCaused);
         knightControl.isAttackedByEnemy();
      }
   }
}
