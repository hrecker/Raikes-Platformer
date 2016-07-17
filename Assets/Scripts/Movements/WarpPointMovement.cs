using System;
using UnityEngine;

public class WarpPointMovement: MonoBehaviour
{
	private IMessenger objectMessenger;
	public Transform warpPoint;
	private GameObject collidedObject;

	public void Start() {
		objectMessenger = GetComponent<IMessenger> ();
		if (objectMessenger == null) {
			objectMessenger = GetComponentInParent<IMessenger> ();
		}
		if (objectMessenger == null) {
			objectMessenger = GetComponentInChildren<IMessenger> ();
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && objectMessenger != null) {
			collidedObject = SceneMessenger.GetMainPlayerObject(other.gameObject);
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == collidedObject) {
			//Player is no longer inside collider
			collidedObject = null;
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) && collidedObject != null) {
			IMessenger otherMessenger = collidedObject.GetComponent<IMessenger> ();
			if (otherMessenger == null) {
				otherMessenger = collidedObject.GetComponentInParent<IMessenger> ();
			}
			if (otherMessenger == null) {
				otherMessenger = collidedObject.GetComponentInChildren<IMessenger> ();
			}
			if (otherMessenger != null) {
				otherMessenger.Invoke (Message.WARPED, new object[] { warpPoint.position, collidedObject });
			}
			objectMessenger.Invoke (Message.WARPED, new object[] { warpPoint.position, collidedObject });
			collidedObject = null;
		}
	}

}

