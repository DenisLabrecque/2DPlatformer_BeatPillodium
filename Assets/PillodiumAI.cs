using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque & James Limantara
// This script controls the boss enemy; it requires that the player be tagged
// as "Player", and the game controller must be assigned to it.

public class PillodiumAI : MonoBehaviour
{
   public  GameControl gameControl;    // Game controller must be assigned to this script
   private GameObject player;          // Player game object for player position
   public  GameObject fireballPrefab;
   private Animator animator;          // Allows changing animation states
   private SpriteRenderer spriteRenderer;
   private CircleCollider2D KillArea;  // Killzone of the Pillodium's attack (animated to expand)
   private float speed = 6.0f;
   private float direction;            // X-position from Pillodium to player
   private float flashTimer = 0.0f;
   private float flashTime = 0.1f;
   private float fireballTimer = 0.0f; // Fireball reload counter
   private float fireballReload = 2.0f;// Fireball shot rate time
   private float randomAttackTime;     // Timer that makes two attacks random
   private float randomAttackCounter;
   private int   hitpoints = 100;
   private int   scoreValue = 150;
   private int   attackType;           // 1 is fireball, 2 is wings
   private bool  isAwake = false;
   private bool  isDead = false;
   private bool  isAttacked = false;

   void Start()
   {
      // Initialize the variables
      animator       = GetComponent<Animator>();
      player         = GameObject.FindWithTag("Player");
      spriteRenderer = GetComponent<SpriteRenderer>();

      attackType       = Random.Range(1, 2);
      randomAttackTime = Random.Range(5.0f, 12.0f);
   }

   void Update()
   {
      randomAttackCounter += Time.deltaTime;
      flashTimer          += Time.deltaTime;
      fireballTimer       += Time.deltaTime;

      // Check whether the pillodium is dead
      if (hitpoints <= 0)
      {
         // Block controls and tell the player he won
         gameControl.gameIsWon();

         // Make the Pillodium die and disappear
         dieWhenKilled();
      }
      else
      {
         // Make the pillodium follow the player if alerted and alive
         harassPlayer();

         // Flash when attacked
         if (isAttacked)
         {
            spriteRenderer.enabled = false;
            isAttacked = false;
            flashTimer = 0.0f;
         }
         else if ((flashTimer > flashTime) && !spriteRenderer.enabled)
         {
            spriteRenderer.enabled = true;
         }
      }
   }

   void dieWhenKilled()
   {
      // Play the death animation
      animator.SetInteger("state", 2);

      // Pillodium falls through the ground
      Destroy(GetComponent<BoxCollider2D>());
      Destroy(GetComponent<CircleCollider2D>());
      Component[] head = GetComponentsInChildren<CircleCollider2D>();
      foreach (CircleCollider2D damageArea in head)
      {
         Destroy(damageArea);
      }

      // Pillodium deleted when out of bounds
      Destroy(gameObject, 3.0f);

      if(!isDead)
      {
         player.GetComponent<knightControl>().addScorePoints(scoreValue);
         isDead = true;
      }
   }

   // Called in Update; decides between attacks
   void harassPlayer()
   {
      if (isAwake)
      {
         // Fireball attack (attackType 1)
         if (attackType == 1)
         {
            // Shoot at specified interval
            if (fireballTimer > fireballReload)
            {
               GameObject fireball;
               Vector2 fireballPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
               fireball = (GameObject)Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
               fireballTimer = 0.0f;
            }
         }
         // Wing attack (attackType 2)
         else
         {
            // Only move if Pillodium/player are farther than 1 (avoids jitter)
            if (transform.position.x - player.transform.position.x > 1.0f || player.transform.position.x - transform.position.x > 1.0f)
            {
               if (transform.position.x > player.transform.position.x)
               // The Pillodium needs to go left
               {
                  transform.Translate(-speed * Time.smoothDeltaTime, 0, 0);
               }
               else
               // The Pillodium needs to go right (always moving)
               {
                  transform.Translate(speed * Time.smoothDeltaTime, 0, 0);
               }
            }
         }

         if (randomAttackCounter >= randomAttackTime)
         {
            randomAttackCounter = 0.0f;
            randomAttackTime = Random.Range(5.0f, 12.0f);
            if (attackType == 1)
            {
               attackType = 2;
            }
            else
            {
               attackType = 1;
            }
         }
      }

   }

   // Called when the player enter Pillodium's area
   public void awakenPillodium()
   {
      // Pillodium starts swinging his arms
      animator.SetInteger("state", 1);
      isAwake = true;
   }

   // Called by the player to attack the Pillodium
   public void damage(int damageAmount)
   {
      hitpoints -= damageAmount;
      isAttacked = true;
   }
}