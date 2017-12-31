using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denis Labrecque
// This script follows the player as he moves horizontally across the scenery.
// It does not move vertically when the player jumps. This script is part of the
// camera, which must be aligned with the player at the beginning of the game.
// This script needs to be assigned the player (target) game object to follow.

public class FollowPlayerHorizontally : MonoBehaviour {

   public GameObject target; // Player must be connected to the camera

   private Vector3 cameraDifference; // Distance between target and camera

	// Use this for initialization
	void Start () {
		cameraDifference = transform.position - target.transform.position;
	}
	
	// Tracks objects that move inside update
	void LateUpdate () {
      // Sets the camera to follow the target horizontally, and sets vertical offset and z depth
		transform.position = new Vector3(target.transform.position.x + cameraDifference.x, 0.0f, -100);
	}
}
