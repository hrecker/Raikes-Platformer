using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public HorizontalDirection spawnDirection;
    public bool isBuzzword;
    public string[] buzzwordStrings;
    public bool repeating;
    public float repeatTime;

    private GameObject spawn;
    private bool activated;
    private float currentRepeatTimePassed;

    void Update() 
    {
        if(activated && repeating)
        {
            currentRepeatTimePassed += Time.deltaTime;
            if(currentRepeatTimePassed >= repeatTime)
            {
                currentRepeatTimePassed = 0;
                spawnEnemy();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Rigidbody2D otherRigidbody = other.GetComponentInParent<Rigidbody2D> ();
		if (other.tag == "SpawnCollider" && spawn == null)
        {
            if (rigidbodyIsMovingInCorrectDirection(otherRigidbody, other))
            {
                spawnEnemy();
                activated = true;
            }
            else
            {
                activated = false;
            }
        }
    }

	bool rigidbodyIsMovingInCorrectDirection(Rigidbody2D rigidbody, Collider2D other)
    {
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

    private void spawnEnemy()
    {
        spawn = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
        IDirected directedComponent = spawn.GetComponent<IDirected>();
        if (directedComponent != null)
        {
            directedComponent.horizontalDirection = spawnDirection;
        }
        if (isBuzzword)
        {
            GenericBuzzwordMessenger buzzwordMessenger = spawn.GetComponent<GenericBuzzwordMessenger>();
            if (buzzwordMessenger == null)
            {
                Debug.LogError("Spawn point marked for buzzword, but GenericBuzzwordMessenger was not found on the enemyPrefab");
            }
            else
            {
                buzzwordMessenger.buzzword = buzzwordStrings[Random.Range(0, buzzwordStrings.Length - 1)];
            }
        }
    }
}
