using UnityEngine;

public class PlatformHitbox: MonoBehaviour
{
	private IMessenger objectMessenger;
	private BoxCollider2D boxCollider;
	//This is used to allow the player but not
	//enemies to trigger certain platforms and
	//allow all objects to trigger other platforms.
	public bool restrictToValidObjects = true;

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
		if (other.isTrigger && objectMessenger != null && hitbox != null && (hitbox.affectsPlatforms || !restrictToValidObjects))
		{
			objectMessenger.Invoke(Message.PLATFORM_LANDED_ON, new object[] { other });
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Hitbox hitbox = other.GetComponent<Hitbox> ();
		if (other.isTrigger && objectMessenger != null && hitbox != null && (hitbox.affectsPlatforms || !restrictToValidObjects))
		{
			objectMessenger.Invoke(Message.PLATFORM_JUMPED_OFF_OF, new object[] { other });
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