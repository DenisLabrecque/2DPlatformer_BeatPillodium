using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Limantara
// Change start position and rotation of the fireball, then move it towards the player

public class fireball : MonoBehaviour
{
   private float elapsedTime = 0.0f;
   private GameObject player;           // The player object
   private knightControl knightControl; // Control script attached to the player
   private int fireballDamage = 7;      // Fireball damage to player

   // Use this for initialization
   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player");
      knightControl = player.GetComponent<knightControl>();

      // Compare fireball and player position, set fireball position and rotation
      if (transform.position.x > player.transform.position.x)
      {
         transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y);
         transform.rotation = Quaternion.identity;
      }
      else
      {
         transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y);
         transform.rotation = Quaternion.Euler(0, 180, 0);
      }
   }

   // Update is called once per frame
   void Update()
   {
      elapsedTime += Time.deltaTime;

      // Move the fireball on x axis
      transform.Translate(-10 * Time.deltaTime, 0, 0);

      Destroy(gameObject, 3.0f);
   }

   // Deal damage to the player and destroy fireball
   void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.tag == "Player")
      {
         knightControl.isAttackedByEnemy();
         knightControl.GetComponent<knightControl>().attackPlayer(fireballDamage);

      }

      Destroy(gameObject);
   }
}