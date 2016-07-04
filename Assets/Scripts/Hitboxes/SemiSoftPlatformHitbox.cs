using UnityEngine;

public class SemiSoftPlatformHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;
	private BoxCollider2D boxCollider;
	private float groundMargin = 0.20f;

	void Start()
	{
		objectMessenger = this.getMessenger ();
		boxCollider = this.GetAdjacentComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.tag == "Player")
        {
            updateCollisionState(other);
        }
	}

	void OnTriggerStay2D(Collider2D other)
	{
        if(other.gameObject.tag == "Player")
        {
            updateCollisionState(other);
        }
	}

	private void updateCollisionState(Collider2D other)
	{
        if (transform.parent == null || other.transform.parent == null)
        {
            return;
		}

		BoxCollider2D thisParentCollider  = transform.parent.GetComponent<BoxCollider2D> ();
		BoxCollider2D otherParentCollider = other.transform.parent.GetComponent<BoxCollider2D> ();
		float otherY = otherParentCollider.bounds.center.y - otherParentCollider.bounds.extents.y;
		float thisY = thisParentCollider.bounds.center.y + thisParentCollider.bounds.extents.y;
		if (otherY >= thisY - groundMargin && ColliderIsValidHitbox (other)) {
				Physics2D.IgnoreCollision (thisParentCollider, otherParentCollider, false);
			} else {
				Physics2D.IgnoreCollision (thisParentCollider, otherParentCollider, true);
			}
	}

	private bool ColliderIsValidHitbox(Collider2D other)
	{
		if (other.transform.parent == null) {
			return false;
		}

		Hitbox[] hitboxes = other.transform.parent.GetComponentsInChildren<Hitbox>();
		Hitbox hitbox = null;
		foreach (Hitbox box in hitboxes) {
			Collider2D currentCollider = box.GetComponent<Collider2D> ();
			if (currentCollider.offset.y < 0.0f) {
				hitbox = box;
				break;
			}
		}
		//We only consider foot hitboxes to be valid,
		//so we return false if the collider is set
		//above the game object's center.
		return hitbox != null/* && other.offset.y < 0.0*/;
	}
}

