using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Limantara
// Contains the movement controls for the player (Knight). This must be assigned
// the Game Controller and a Blue Fireball attack prefab.

public class knightControl : MonoBehaviour
{
   public  GameControl gameControl;
   public  GameObject  blueFireballPrefab;
   private Animator animator;
   private SpriteRenderer spriteRenderer;
   private float flashTimer     = 0.0f;
   private float flashTime      = 0.1f; // Character flashes when attacked
   private float slashTimer     = 0.0f;
   private float slashCooldown  = 1.0f; // Slash attack has 1 second interval
   private float fireballTimer  = 0.0f;
   private float fireballReload = 0.6f; // Blue Fireball reload interval
   private int   scorePoints    = 0;    // Player score
   private int   speed;                 // The speed of the character moving
   public  int   health;                // Current health of the character
   private bool  isAttacked = false;    // Tell if the character is attacked by enemy
   private bool  onGround;              // Tell if the character is on ground

   // Use this for initialization
   void Start()
   {
      animator = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      onGround = false;
      speed = 5;
      health = 100;
   }

   // Update is called once per frame
   void Update()
   {
      fireballTimer += Time.deltaTime;
      slashTimer    += Time.deltaTime;
      flashTimer    += Time.deltaTime;

      if (gameControl.getGamePlayable())
      {
         // Move right
         if (Input.GetKey(KeyCode.RightArrow))
         {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (speed <= 5)
               animator.SetInteger("state", 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);

         }
         else if (Input.GetKeyUp(KeyCode.RightArrow))
         {
            animator.SetInteger("state", 0);
         }

         // Move left
         if (Input.GetKey(KeyCode.LeftArrow))
         {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            if (speed <= 5)
               animator.SetInteger("state", 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);

         }
         else if (Input.GetKeyUp(KeyCode.LeftArrow))
         {
            animator.SetInteger("state", 0);
         }

         // Ground check and sprint
         if (GetComponent<Rigidbody2D>().velocity.y >= -1.0f && GetComponent<Rigidbody2D>().velocity.y <= 1.0f)
         {
            if (Input.GetKey(KeyCode.LeftShift))
            {
               speed = 10;
               animator.SetInteger("state", 2);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
               speed = 5;
               animator.SetInteger("state", 0);
            }


            // Ground check and jump
            onGround = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
               onGround = false;
               animator.SetBool("jump", true);
               GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            }

            if (onGround)
            {
               animator.SetBool("jump", false);
            }
         }

         // Slash Attack (James)
         if (Input.GetKeyDown(KeyCode.Z))
         {
            if (slashTimer > slashCooldown)
            {
               animator.SetBool("slash", true);
               slashTimer = 0.0f;
            }
         }

         // Blue Fireball attack (Denis)
         if (Input.GetKeyDown(KeyCode.X) && fireballTimer >= fireballReload)
         {
            fireballTimer = 0.0f;

            // Shoot based on the player's facing directon
            if (transform.rotation == Quaternion.Euler(0, 0, 0)) // Facing right
            {
               // Instantiate a Blue Fireball right
               Vector3 shotPosition = transform.position + new Vector3(0.44f, -0.2f, 0);
               GameObject blueFireball = (GameObject)Instantiate(blueFireballPrefab, shotPosition, Quaternion.identity);
               blueFireball.GetComponent<BlueFireball>().setDirection(1);
            }
            else // Player facing left
            {
               // Instantiate a Blue Fireball left
               Vector3 shotPosition = transform.position + new Vector3(-0.44f, -0.2f, 0);
               GameObject blueFireball = (GameObject)Instantiate(blueFireballPrefab, shotPosition, Quaternion.identity);
               blueFireball.GetComponent<BlueFireball>().setDirection(-1);
            }
         }

         if (slashTimer > 0.8f)
            animator.SetBool("slash", false);

         // Flash when attacked (James)
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

   public void isAttackedByEnemy()
   {
      isAttacked = true;
   }

   public int getHealth()
   {
      return health;
   }

   public int getScorePoints()
   {
      return scorePoints;
   }

   // Give the player more points
   public void addScorePoints(int score)
   {
      scorePoints += score;
   }

   // Called by an enemy to attack the Knight
   public void attackPlayer(int attackValue)
   {
      health -= attackValue;
   }
}