using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// GAME SETTING
// A tropical forest is populated with enemies that the player
// must eliminate to achieve the objective.
//
// CREDITS
//
// * Large forest background: Julien Jorge,    opengameart.org/content/large-forest-background
// * Palm Tree Clipart #8043: clipart393,      clipartix.com/palm-tree-clipart-image-8043/
// * Tin Can:                 Rick and Morty,  rickandmorty.wikia.com/wiki/Tin_Can
// * Krubbish:                Ploaj, Teawater, spriters-resource.com/3ds/marioluigidreamteam/sheet/63133/
// * Pillodium:               Ploaj, Teawater, spriters-resource.com/3ds/marioluigidreamteam/sheet/64924/
// * Skeleton:                irmirx,          opengameart.org/content/skeleton-animations
// * Knight:                  pzUH,            opengameart.org/content/the-knight-free-sprite
// * Fireball Spell:          Clint Bellanger, opengameart.org/content/fireball-spell
// * Blue Fireball:           codekatana,      sites.google.com/site/codekatana/resources
// * Health Bar:              Ben Reynolds,    as3gametuts.com/2012/02/10/actionscript-health-bar-tutorial/   


public class GameControl : MonoBehaviour
{

   public  Text txtIntroText;       // Countdown text
   public  Text txtPointText;       // Player score text
   public  Text txtDamageText;      // Player health text
   public  Text txtInstructionText; // Player key instructions
   public  Image healthBar;
   private GameObject player;
   private knightControl knightControl;
   private int displayHealth = 100;
   private float timeSinceUpdate;
   private float gameStartCounter = 3.0f;
   private static bool gamePlayable = false;
   private bool gameWon = false;

   void Start()
   {
      player        = GameObject.FindWithTag("Player");
      knightControl = player.GetComponent<knightControl>();

      txtInstructionText.text = "Arrow keys, left-shift, and spacebar make you walk, run, and jump; Z performs a swordslash, and X shoots a fireball.";

      showScores();
   }

   void Update()
   {
      checkGameCondition();
      showScores();
   }

   void checkGameCondition()
   {
      // Wait for the counter to count down at beginning of game
      if (gameStartCounter > 0.0f)
      {
         txtIntroText.text = ((int)gameStartCounter + 1.0f).ToString();
         gameStartCounter -= Time.deltaTime;
      }
      else {
         if (gameWon)
         {
            // Deactivate and say game is won
            txtIntroText.text = "You win!";
            Time.timeScale = 0; // Freeze screen
            gamePlayable = false;
         }
         else if (knightControl.getHealth() <= 0)
         {
            // Deactivate and say game is lost
            txtIntroText.text       = "Try again?";
            txtInstructionText.text = "Hit Enter to retry.";
            Time.timeScale = 0; // Freeze screen
            gamePlayable = false;

            // Reload the current scene on player Enter command
            if (Input.GetButtonDown("Submit"))
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               Time.timeScale = 1; // Unfreeze screen
            }
         }
         else
         {
            // Make game playable after the countdown
            txtIntroText.text       = null;
            txtInstructionText.text = null;
            gamePlayable = true;
         }
      }
   }

   // Called every frame
   void showScores()
   {
      txtPointText.text = knightControl.getScorePoints().ToString();
      txtDamageText.text = knightControl.getHealth().ToString();
      if (knightControl.getHealth() < displayHealth)
      {
         if (timeSinceUpdate >= .1f)
         {
            displayHealth--;
            timeSinceUpdate = 0f;
            healthBar.fillAmount = displayHealth / 100.0f;
         }
         else
         {
            timeSinceUpdate += (3 * Time.deltaTime);
         }
      }

   }

   // Called by the boss when he dies (win condition)
   public void gameIsWon()
   {
      gameWon = true;
   }

   public bool getGamePlayable()
   {
      return gamePlayable;
   }
}

// PLEDGE
//
// This is our own first time work. We did not copy any code or others' scripts/sprites unless
// it was noted in our game controller and it was approved by our teacher.
// 
// Denis Labrecque
//
// ________________________
//
// James Limantara
//
// ________________________