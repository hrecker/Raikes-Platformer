using System;
using UnityEngine;

public interface IMovement {

	HorizontalDirection Direction { get; set; }

}

public class BuzzwordEnemyMovement: MonoBehaviour, IMovement
{

	public HorizontalDirection direction;
	public HorizontalDirection Direction {
		get { return this.direction; }
		set { this.direction = value; }
	}
	public float horizontalSpeed;
	public float firingDelay;
	public float projectileSpeed;
	private float firingTimePassed;
	private ObjectSpawner projectileSpawner;
	private Rigidbody2D rigidbodyObject;
	private IMessenger messenger;

	public void Start() {
		this.rigidbodyObject = this.GetComponent<Rigidbody2D> ();
		this.messenger = this.GetComponent<IMessenger>();
		this.projectileSpawner = this.GetComponent<ObjectSpawner> ();
//		this.projectileSpawner.DirectionSource = this;
	}

	void Update() {
		this.rigidbodyObject.velocity = new Vector2((float) this.direction * this.horizontalSpeed, this.rigidbodyObject.velocity.y);

		this.firingTimePassed += Time.deltaTime;
		if (this.firingTimePassed >= this.firingDelay) {
			this.firingTimePassed -= this.firingDelay;
			Rigidbody2D projectile = this.projectileSpawner.spawnObject();
			ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement> ();
			projectileMovement.speed = this.projectileSpeed;
			projectileMovement.MoveInDirection (this.direction);
			this.messenger.Invoke ("Projectile Fired", null);
		}
	}
}
