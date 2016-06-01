using System;
using UnityEngine;

public class ObjectSpawner: MonoBehaviour
{
	public GameObject objectToSpawn;
	public Vector2 spawnOffset;
	/// <summary>
	/// The direction to spawn in. If NONE, then
	/// the direction of the parent object is used.
	/// </summary>
	public HorizontalDirection spawnDirection;

	public GameObject SpawnObject(Vector2 position, Vector2 offset, HorizontalDirection spawnDirection)
    {
        GameObject obj = Instantiate(objectToSpawn, new Vector3(position.x + offset.x * (float)spawnDirection,
			position.y + offset.y), Quaternion.identity) as GameObject;
        return obj;
	}

	public GameObject SpawnObject()
    {
		Vector2 parentPosition = this.GetComponent<BoxCollider2D> ().transform.position;
		HorizontalDirection spawnDirection = this.spawnDirection;
		if (spawnDirection == HorizontalDirection.NONE)
        {
			spawnDirection = GetComponent<IMovement> ().Direction;
		}
		return SpawnObject (parentPosition, this.spawnOffset, spawnDirection);
	}
}
