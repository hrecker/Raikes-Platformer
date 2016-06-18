using UnityEngine;

public class ObjectSpawner: MonoBehaviour, IDirected
{
	public GameObject objectToSpawn;
	public Vector2 spawnOffset;
	// direction to spawn in. If NONE, then the direction of the parent object is used.
	public HorizontalDirection horizontalSpawnDirection;
	public VerticalDirection verticalSpawnDirection;

    public HorizontalDirection horizontalDirection
    {
        get { return horizontalSpawnDirection; }
        set { horizontalSpawnDirection = value; }
    }

    public VerticalDirection verticalDirection
    {
		get { return verticalSpawnDirection; }
		set { verticalSpawnDirection = value; }
    }

    public GameObject SpawnObject(Vector2 position, Vector2 offset, HorizontalDirection spawnDirection)
    {
        GameObject obj = Instantiate(objectToSpawn, new Vector3(position.x + offset.x * (float)horizontalSpawnDirection,
			position.y + offset.y * (float)verticalSpawnDirection), Quaternion.identity) as GameObject;
        return obj;
	}

	public GameObject SpawnObject()
    {
		Vector2 parentPosition = GetComponent<BoxCollider2D> ().transform.position;
		HorizontalDirection spawnDirection = horizontalSpawnDirection;
		if (spawnDirection == HorizontalDirection.NONE)
        {
			spawnDirection = GetComponent<IDirected> ().horizontalDirection;
		}
		return SpawnObject (parentPosition, spawnOffset, spawnDirection);
	}
}
