using System;
using UnityEngine;

public class ProjectileMovement: MonoBehaviour
{

	public HorizontalDirection direction;
	public float speed;
	public float duration;
	private float timePassed;
	private IMessenger messenger;

	public void Start() {
		this.messenger = this.GetComponent<IMessenger> ();
	}

	public void MoveInDirection(HorizontalDirection direction) {
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.speed * (float)direction, 0.0f);
		this.direction = direction;
	}

	public void Update() {
		this.timePassed += Time.deltaTime;
		if (this.timePassed >= this.duration) {
			this.messenger.Invoke (Message.PROJECTILE_EXPIRED, null);
			this.Expire ();
		}
	}

	public void Expire() {
		Destroy (this.gameObject);
	}

}
