using System;
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
		this.objectMessenger = this.GetComponent<IMessenger>();
		if (objectMessenger == null)
		{
			this.objectMessenger = this.GetComponentInParent<IMessenger>();
		}
		if (objectMessenger == null)
		{
			this.objectMessenger = this.GetComponentInChildren<IMessenger>();
		}
	}

	void Update() {
		if (this.collapseCount > 0.0f) {
			this.collapseCount -= Time.deltaTime;
			if (this.collapseCount <= 0.0f) {
				this.Collapse ();
			}
		} else if (this.respawnCount > 0.0f) {
			this.respawnCount -= Time.deltaTime;
			if (this.respawnCount <= 0.0f) {
				this.Respawn ();
			}
		}
	}

	public void BeginCollapse() {
		//We don't want to reset the collapse count if the
		//player jumps and lands on the platform twice.
		if (this.collapseCount <= 0.0f) {
			this.collapseCount = this.timeUntilCollapse;
		}
	}

	public void Collapse() {
		this.objectMessenger.Invoke (Message.PLATFORM_COLLAPSED, null);
		this.respawnCount = this.timeUntilRespawn;
	}

	public void Respawn() {
		this.objectMessenger.Invoke (Message.PLATFORM_RESPAWNED, null);
	}

}