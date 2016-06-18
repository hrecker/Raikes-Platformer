using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public HorizontalDirection spawnDirection;

    private GameObject spawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnCollider" && spawn == null)
        {
            spawn = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
            IDirected directedComponent = spawn.GetComponent<IDirected>();
            if(directedComponent != null)
            {
                directedComponent.horizontalDirection = spawnDirection;
            }
        }
    }
}
