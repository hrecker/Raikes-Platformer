using UnityEngine;

public class PlatformHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;
	private BoxCollider2D boxCollider;

	void Start()
	{
		objectMessenger = GetComponent<IMessenger>();
		if(objectMessenger == null)
		{
			objectMessenger = GetComponentInParent<IMessenger>();
		}
		if (objectMessenger == null)
		{
			objectMessenger = GetComponentInChildren<IMessenger>();
		}
		boxCollider = this.GetComponentInHierarchy<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Hitbox hitbox = other.GetComponent<Hitbox> ();
		if (other.isTrigger && objectMessenger != null && hitbox != null && hitbox.affectsPlatforms)
		{
			objectMessenger.Invoke(Message.PLATFORM_LANDED_ON, new object[] { other });
		}
	}

	public void Deactivate()
	{
		boxCollider.enabled = false;
	}

	public void Activate()
	{
		boxCollider.enabled = true;
	}
}