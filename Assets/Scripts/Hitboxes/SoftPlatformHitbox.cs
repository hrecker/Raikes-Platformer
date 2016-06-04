using System;
using UnityEngine;

public class SoftPlatformHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;
	private BoxCollider2D boxCollider;
	private float groundMargin = 0.20f;

	void Start()
	{
		this.objectMessenger = this.getMessenger ();
		this.boxCollider = this.GetAdjacentComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		this.updateCollisionState (other);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		this.updateCollisionState (other);
	}
		
	private void updateCollisionState(Collider2D other) {
		float otherY = other.bounds.center.y - other.bounds.extents.y;
		float thisY = this.boxCollider.bounds.center.y + this.boxCollider.bounds.extents.y;
		if (this.transform.parent == null || other.transform.parent == null) {
			return;
		}
		BoxCollider2D thisParentCollider  = this.transform.parent.GetComponent<BoxCollider2D> ();
		BoxCollider2D otherParentCollider = other.transform.parent.GetComponent<BoxCollider2D> ();
		if (otherY >= thisY - this.groundMargin && other.GetAdjacentComponent<Hitbox> () != null) {
			Physics2D.IgnoreCollision(thisParentCollider, otherParentCollider, false);
		} else {
			Physics2D.IgnoreCollision(thisParentCollider, otherParentCollider, true);
		}
	}
}

