using System;
using UnityEngine;

public class CollapsingPlatformMessenger: MonoBehaviour, IMessenger
{

	private CollapsingPlatformMovement movement;
	private PlatformHitbox hitbox;
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;

	public void Start() {
		this.movement = this.GetComponent<CollapsingPlatformMovement>();
		this.hitbox = this.GetComponentInChildren<PlatformHitbox> ();
		this.boxCollider = this.GetComponent<BoxCollider2D>();
		this.spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}

	public void Invoke(Message msg, object[] objects) {
		switch (msg) {
		case Message.PLATFORM_LANDED_ON:
			this.movement.BeginCollapse ();
			break;
		case Message.PLATFORM_COLLAPSED:
			this.hitbox.Deactivate ();
			this.boxCollider.enabled = false;
			this.spriteRenderer.enabled = false;
			break;
		case Message.PLATFORM_RESPAWNED:
			this.hitbox.Activate ();
			this.boxCollider.enabled = true;
			this.spriteRenderer.enabled = true;
			break;
		default:
			break;
		}
	}
}

