using System;
using UnityEngine;

public class PlatformSideHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;

	public void Start()
	{
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
		IMessenger otherMessenger = other.GetComponent<IMessenger> ();
		if (otherMessenger == null) {
			otherMessenger = other.GetComponentInParent<IMessenger> ();
		}
		if (otherMessenger == null) {
			otherMessenger = other.GetComponentInChildren<IMessenger> ();
		}
		if (otherMessenger != null) {
			otherMessenger.Invoke (Message.HIT_SIDE_OF_PLATFORM, null);
		}
	}

}

