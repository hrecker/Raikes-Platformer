using System;
using UnityEngine;

public class PowerupBlockSpawner: ObjectSpawner
{
	public float spawnSpeed = 4.0f;

	public override GameObject SpawnObject(Vector2 position, Vector2 offset, HorizontalDirection spawnDirection)
	{
		GameObject obj = base.SpawnObject (position, offset, spawnDirection);
		Rigidbody2D rigidbody = obj.GetComponent<Rigidbody2D> ();
		if (rigidbody != null) {
			rigidbody.velocity = new Vector2 (0.0f, spawnSpeed);
		}
		return obj;
	}
}

