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
		Hitbox hitbox = other.GetComponent<Hitbox> ();
		if (other.isTrigger && this.objectMessenger != null && hitbox != null && hitbox.affectsPlatforms)
		{
			this.objectMessenger.Invoke(Message.PLATFORM_LANDED_ON, new object[] { other });
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