using System;
using UnityEngine;

public class ObjectSpawner: MonoBehaviour
{

	public Rigidbody2D objectToSpawn;
	public Vector2 spawnOffset;
	/// <summary>
	/// The direction to spawn in. If NONE, then
	/// the direction of the parent object is used.
	/// </summary>
	public Direction spawnDirection;

	public void Start() {

	}

	public Rigidbody2D spawnObject(Vector2 position, Vector2 offset, Direction spawnDirection) {
		Rigidbody2D obj = Instantiate(this.objectToSpawn, new Vector3(position.x + offset.x * (float)spawnDirection,
			position.y + offset.y), Quaternion.identity) as Rigidbody2D;
		return obj;
	}

	public Rigidbody2D spawnObject() {
		Vector2 parentPosition = this.GetComponent<BoxCollider2D> ().transform.position;
		Direction spawnDirection = this.spawnDirection;
		if (spawnDirection == Direction.NONE) {
			spawnDirection = this.GetComponent<IMovement> ().Direction;
		}
		return this.spawnObject (parentPosition, this.spawnOffset, spawnDirection);
	}

}
