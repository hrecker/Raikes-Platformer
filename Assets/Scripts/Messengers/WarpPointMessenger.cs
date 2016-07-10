using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpPointMessenger: MonoBehaviour, IMessenger
{

	public void Invoke(Message msg, object[] args) {
		switch (msg) {
		case Message.WARPED:
			Vector2 warpPoint = (Vector2)args [0];
			GameObject player = (GameObject)args [1];
			Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D> ();
			if (rigidbody != null)
			{
				rigidbody.transform.position = new Vector3 (warpPoint.x, warpPoint.y, 0.0f);
				//Set the velocity to 0, because otherwise, the player
				//continues moving in the direction they were before warping.
				rigidbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			}
			break;
		default:
			break;
		}
	}
}

