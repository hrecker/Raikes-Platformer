using System;
using UnityEngine;

public class WarpPointExit: MonoBehaviour
{
	public int warpTag;

	public void Start() {
		
		foreach (var g in GameObject.FindGameObjectsWithTag ("Player")) {
			//We want to find the main player object, not any of its children.
			//Thus, the player object without a parent is thus the main object.
			if (g.gameObject.transform.parent == null) {
				PlayerMovement movement = g.GetComponent<PlayerMovement> ();
				//We want to allow multiple warp points in a single level,
				//so we validate that the player is warping to this specific point.
				if (movement != null && movement.warpTag == warpTag) {
					ApplyState (g);
				}
				break;
			}
		}
	}

	public void ApplyState(GameObject gameObject) {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
		gameObject.transform.position = transform.position;
	}

}

