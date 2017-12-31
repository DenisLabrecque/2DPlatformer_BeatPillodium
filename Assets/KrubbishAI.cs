using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script must have a Tin Can prefab assigned to it. Because the Krubbish
// moves unidirectionally, objects called "Finish" cause the Krubbish to be
// deleted after falling through the ground.

public class KrubbishAI : MonoBehaviour {

   public  GameObject tinCanPrefab; // Prefab that must be assigned
   private GameObject player;       // Player that is tagged "Player"
   private float speed = 0.0f;      // Default is changed when awakened
   private float fireRate;          // Reload time (random for each shot)
   private float fireRateRangeStart = 0.5f;
   private float fireRateRangeEnd = 2.2f;
   private float fireTimer;         // Time since the last shot
   private int   hitpoints = 7;
   private int   scoreValue = 10;
   private bool  playerIsInAttackArea = false;
   private bool  isDead = false;

   void Start () {
      fireRate  = Random.Range(fireRateRangeStart, fireRateRangeEnd);
      fireTimer = fireRate; // Krubbish can shoot as soon as player enters zone
      player    = GameObject.FindWithTag("Player"); // Get the player for giving score
   }
	
	void Update () {
      fireTimer += Time.deltaTime;

      if (hitpoints <= 0)
      {
         dieWhenKilled();
      }
      else
      {
         moveForward();
      }

      // Shoot garbage at the player
      if (playerIsInAttackArea && fireTimer >= fireRate) {
         // Instantiate tin can in front of Krubbish
         Vector3 tinCanShotPosition = transform.position + new Vector3(-0.34f, 0.5f, 0);
         GameObject tinCan = (GameObject)Instantiate(tinCanPrefab, tinCanShotPosition, Quaternion.identity);

         // Create a random time for the next shot to occur
         fireRate = Random.Range(fireRateRangeStart, fireRateRangeEnd);
         fireTimer = 0.0f;
      }
	}

   // Make the Krubbish disappear once it hits the end of the play area
   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.tag == "Finish")
      {
         dieWhenKilled();
      }
   }

   void dieWhenKilled() {
      // Krubbish falls through the ground
      Destroy(GetComponent<BoxCollider2D>());
      Component[] eyes = GetComponentsInChildren<BoxCollider2D>();
      foreach (BoxCollider2D damageArea in eyes) {
         Destroy(damageArea);
      }

      // Krubbish deleted when out of bounds
      Destroy(gameObject, 3.0f);

      // Add score to player when the Krubbish dies
      if (!isDead)
      {
         player.GetComponent<knightControl>().addScorePoints(scoreValue);
         isDead = true;
      }
   }

   // Start the Krubbish (called by Start Shooting Garbage)
   public void awaken()
   {
      speed = 0.8f;
   }

   void moveForward() {
      transform.Translate(-speed * Time.smoothDeltaTime, 0, 0);
   }

   public void shootGarbage(bool shoot) {
      playerIsInAttackArea = shoot;
   }

   // Attack the player calls against the Krubbish
   public void damage(int damageAmount)
   {
      hitpoints -= damageAmount;
   }
}
