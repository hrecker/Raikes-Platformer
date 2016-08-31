using UnityEngine;

public class IanProjectileSpawner : MonoBehaviour
{
    public float firingDelay;
    public float projectileSpeed;

    private const HorizontalDirection spawnDirection = HorizontalDirection.RIGHT;
    private float firingTimePassed;
    private ObjectSpawner projectileSpawner;

    public void Start() 
    {
        if (projectileSpawner == null) 
        {
            projectileSpawner = GetComponent<ObjectSpawner>();
        }
    }

    void Update() 
    {
        firingTimePassed += Time.deltaTime;
        if (firingTimePassed >= firingDelay) 
        {
            firingTimePassed = 0;
            GameObject projectile = projectileSpawner.SpawnObject();
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.speed = projectileSpeed;
            projectileMovement.horizontalDirection = spawnDirection;
        }
    }
}
