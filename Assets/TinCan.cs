using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script defines the Tin Can prefab shot by a Krubbish enemy;
// it requires that the player be tagged "Player".

public class TinCan : MonoBehaviour {

   private GameObject player;
   private knightControl knightControl;
   private int   damageCaused = 2;     // Damage of a Tin Can against the player
   private float destroyDelay = 10.0f; // Time after which tin can disappears
   private float speed;                // Speed of a Tin Can through the air

   void Start () {
      player        = GameObject.FindWithTag("Player");
      knightControl = player.GetComponent<knightControl>();
      speed         = Random.Range(7.0f, 10.0f); // Each tin can has a different speed

      Destroy(gameObject, destroyDelay); // Tin Can exists maximum 10 seconds
   }

   void Update ()
   {
      transform.Translate(-speed * Time.deltaTime, 0, 0);
   }

   void OnTriggerEnter2D(Collider2D collision) {
      // Hurt the player
      if (collision.gameObject.tag == "Player")
      {
         knightControl.attackPlayer(damageCaused);
         Destroy(gameObject);
      }
   }
}
