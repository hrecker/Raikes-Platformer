using System;
using UnityEngine;

public class PlatformHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;
	private BoxCollider2D boxCollider;

	void Start()
	{
		this.objectMessenger = GetComponent<IMessenger>();
		if(objectMessenger == null)
		{
			this.objectMessenger = GetComponentInParent<IMessenger>();
		}
		if (objectMessenger == null)
		{
			this.objectMessenger = GetComponentInChildren<IMessenger>();
		}
		this.boxCollider = this.GetComponentInHierarchy<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		/*float colliderMargin = 0.10f;
		Vector2 bottomLeft = new Vector2 (other.transform.position.x - other.bounds.extents.x, other.bounds.center.y - other.bounds.extents.y - colliderMargin);
		RaycastHit2D result = Physics2D.Raycast (bottomLeft, Vector2.right, boxCollider.bounds.size.x + colliderMargin);
		float y = this.transform.position.y + this.boxCollider.bounds.extents.y / 2.0f;
		Debug.Log ("Bottom: " + bottomLeft.y + " Y: " + y);*/
		Hitbox hitbox = other.GetComponent<Hitbox> ();
		if (other.isTrigger && this.objectMessenger != null && hitbox != null && hitbox.affectsPlatforms)
		{
			this.objectMessenger.Invoke(Message.PLATFORM_LANDED_ON, null);
		}
	}

	public void Deactivate()
	{
		this.boxCollider.enabled = false;
	}

	public void Activate()
	{
		this.boxCollider.enabled = true;
	}
}