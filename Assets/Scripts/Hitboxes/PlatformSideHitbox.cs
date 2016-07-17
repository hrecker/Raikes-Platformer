using System;
using UnityEngine;

/*
 * This hitbox fixes issues with the player colliding with
 * platforms while in the air. Adding a physics material with
 * 0.0 friction stops the player from getting stuck, but as
 * as soon as the player is no longer colliding with the
 * platform, the player jerks forward, because the player's
 * velocity remains non-zero. This hitbox causes the player's
 * (horizontal) velocity to become 0 when colliding with a platform.
 */
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
		
	/*public void OnTriggerEnter2D(Collider2D other)
	{
		SendHitMessage (other);
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		SendHitMessage (other);
	}

	private void SendHitMessage(Collider2D other)
	{
		if (other.GetComponent<PlayerSideFrictionHitbox> () == null) {
//		if (other.GetComponent<Hurtbox>() == null) {
			return;
		}

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
	}*/

}

