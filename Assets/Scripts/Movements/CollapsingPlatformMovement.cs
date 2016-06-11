using UnityEngine;

public class CollapsingPlatformMovement: MonoBehaviour
{
	public float timeUntilCollapse;
	public float timeUntilRespawn;
	private float collapseCount = 0.0f;
	private float respawnCount = 0.0f;
	private IMessenger objectMessenger;

	void Start()
	{
		objectMessenger = this.GetComponent<IMessenger>();
		if (objectMessenger == null)
		{
			objectMessenger = this.GetComponentInParent<IMessenger>();
		}
		if (objectMessenger == null)
		{
			objectMessenger = this.GetComponentInChildren<IMessenger>();
		}
	}

	void Update()
    {
		if (collapseCount > 0.0f)
        {
			collapseCount -= Time.deltaTime;
			if (collapseCount <= 0.0f)
            {
				Collapse ();
			}
		} else if (respawnCount > 0.0f)
        {
			respawnCount -= Time.deltaTime;
			if (respawnCount <= 0.0f)
            {
				Respawn ();
			}
		}
	}

	public void BeginCollapse()
    {
		//We don't want to reset the collapse count if the
		//player jumps and lands on the platform twice.
		if (collapseCount <= 0.0f)
        {
			collapseCount = timeUntilCollapse;
		}
	}

	public void Collapse()
    {
		objectMessenger.Invoke (Message.PLATFORM_COLLAPSED, null);
		respawnCount = timeUntilRespawn;
	}

	public void Respawn()
    {
		objectMessenger.Invoke (Message.PLATFORM_RESPAWNED, null);
	}
}