using System;
using UnityEngine;

public class WarpPointMovement: MonoBehaviour
{
	private IMessenger objectMessenger;
	public int sceneToLoad;
	public int warpTag;

	public void Start() {
		objectMessenger = GetComponent<IMessenger> ();
		if (objectMessenger == null) {
			objectMessenger = GetComponentInParent<IMessenger> ();
		}
		if (objectMessenger == null) {
			objectMessenger = GetComponentInChildren<IMessenger> ();
		}
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && objectMessenger != null) {
			IMessenger otherMessenger = other.GetMessenger ();
			if (otherMessenger != null) {
				otherMessenger.Invoke (Message.WARPED, new object[] { sceneToLoad, warpTag });
			}
			objectMessenger.Invoke (Message.WARPED, new object[] { sceneToLoad, warpTag });
		}
	}

}

