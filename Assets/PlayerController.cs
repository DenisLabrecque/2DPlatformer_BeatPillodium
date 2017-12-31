using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script controls both the player movement and the camera following the player.

public class PlayerController : MonoBehaviour {

   public float speed = 10f; // Speed of the player while walking

	void Update () {
	   // Move the player according to controls
      float run = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
      float jump = Input.GetAxis("Jump") * speed * Time.smoothDeltaTime;
      transform.Translate(run, jump, 0f); // Make the player run and jump
	}
}
