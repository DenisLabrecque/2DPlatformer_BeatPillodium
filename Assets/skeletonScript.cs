using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Limantara
// Controls Skeleton prefab

public class skeletonScript : MonoBehaviour
{
   private bool isAwake;
   private bool isAttacked;
   private bool isDead;
   private float distance; // The distance between the skeleton and the player
   private GameObject player;
   private knightControl knightControl; // Control script attached to the player
   private Animator animator;
   private SpriteRenderer skeletonRenderer;
   public float elapsedTime = 0.0f;
   private int health = 18; // Skeleton's health
   private int scoreValue = 20; // Score given to the player upon death
   private float flashTimer = 0.0f;
   private float flashTime = 0.1f; // Flashes when attacked

   // Use this for initialization
   void Start()
   {
      isAwake = false;
      isAttacked = false;
      isDead = false;
      player = GameObject.FindGameObjectWithTag("Player");
      knightControl = player.GetComponent<knightControl>();
      animator = GetComponent<Animator>();
      skeletonRenderer = GetComponent<SpriteRenderer>();
   }

   // Update is called once per frame
   void Update()
   {
      flashTimer += Time.deltaTime;

      // Distance between player and skeleton
      distance = Vector2.Distance(transform.position, player.transform.position);

      // Awake the skeleton
      if (distance < 10.0f)
      {
         isAwake = true;
      }

      // Moves only when awaken
      if (isAwake)
      {
         if (transform.position.x > player.transform.position.x)
         {
            transform.rotation = Quaternion.identity;
         }
         else
         {
            transform.rotation = Quaternion.Euler(0, 180, 0);
         }

         if (distance > 0.8f)
         {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2 * Time.deltaTime);
            animator.SetInteger("skeletonState", 1);
         }
         else
         {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > Random.Range(1.0f, 10.0f))
            {
               animator.SetInteger("skeletonState", 2);
               elapsedTime = 0.0f;


            }
            else
               animator.SetInteger("skeletonState", 0);
         }
      }

      // Flash when attacked
      if (isAttacked)
      {
         skeletonRenderer.enabled = false;
         isAttacked = false;
         flashTimer = 0.0f;
      }
      else if ((flashTimer > flashTime) && !skeletonRenderer.enabled)
      {
         skeletonRenderer.enabled = true;
      }

      // Play death animation and add scores
      if (health <= 0)
      {
         if (!isDead)
         {
            knightControl.addScorePoints(scoreValue);
            isDead = true;
            Destroy(gameObject, 1.0f);
         }
         else
         {
            animator.SetInteger("skeletonState", -1);
         }
      }
   }

   // The damage to the skeleton
   public void damage(int damage)
   {
      isAttacked = true;
      health -= damage;
   }

   // Deal damage to the player
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.isTrigger != true && other.gameObject.tag == "Player")
      {
         knightControl.isAttackedByEnemy();
         knightControl.attackPlayer(10);

      }
   }
}