using UnityEngine;

public class SoftPlatformHitbox: MonoBehaviour
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
		updateCollisionState (other);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		updateCollisionState (other);
	}
		
	private void updateCollisionState(Collider2D other)
    {
        if (transform.parent == null || other.transform.parent == null)
        {
            return;
        }

        float otherY = other.bounds.center.y - other.bounds.extents.y;
		float thisY = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
		BoxCollider2D thisParentCollider  = transform.parent.GetComponent<BoxCollider2D> ();
		BoxCollider2D otherParentCollider = other.transform.parent.GetComponent<BoxCollider2D> ();
		if (otherY >= thisY - groundMargin && other.GetAdjacentComponent<Hitbox> () != null)
        {
			Physics2D.IgnoreCollision(thisParentCollider, otherParentCollider, false);
		}
        else
        {
			Physics2D.IgnoreCollision(thisParentCollider, otherParentCollider, true);
		}
	}
}

