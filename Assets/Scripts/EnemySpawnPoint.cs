using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public HorizontalDirection spawnDirection;

    private GameObject spawn;

    void OnTriggerEnter2D(Collider2D other)
    {
		Rigidbody2D otherRigidbody = other.GetComponentInParent<Rigidbody2D> ();
		if (other.tag == "SpawnCollider" && spawn == null && rigidbodyIsMovingInCorrectDirection(otherRigidbody, other))
        {
            spawn = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            IDirected directedComponent = spawn.GetComponent<IDirected>();
            if(directedComponent != null)
            {
                directedComponent.horizontalDirection = spawnDirection;
            }
        }
    }

	bool rigidbodyIsMovingInCorrectDirection(Rigidbody2D rigidbody, Collider2D other) {
		Collider2D collider = GetComponent<Collider2D> ();
		//If the other collider's width is less than its height, then it is
		//oriented vertically, and we need to check the relative x-coordinates.
		bool isVerticalCollider = other.bounds.size.x < other.bounds.size.y;
		if (isVerticalCollider)
		{
			if (rigidbody.transform.position.x < collider.transform.position.x)
			{
				//If the rigidbody's x-coodrinate is less than the collider's x-coordinate,
				//(it is to the left of the collider), we only want to spawn an enemy
				//if the rigidbody's x-velocity is greater than 0 (it's moving right).
				return rigidbody.velocity.x > 0.0;
			} else
			{
				return rigidbody.velocity.x < 0.0;
			}
		} else
		{
			if (rigidbody.transform.position.y < collider.transform.position.y)
			{
				return rigidbody.velocity.y > 0.0;
			} else
			{
				return rigidbody.velocity.y < 0.0;
			}
		}
	}
}
