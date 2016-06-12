using UnityEngine;

public class LaundryProjectileSpawner: MonoBehaviour, IDirected
{
    public float firingDelay;
    public float projectileSpeed;
    public HorizontalDirection spawnDirection;

	private float firingTimePassed;
	private ObjectSpawner projectileSpawner;
	private IMessenger messenger;
    private SpriteRenderer spriteRenderer;

    public HorizontalDirection horizontalDirection
    {
        get { return spawnDirection; }
        set
        {
            spawnDirection = value;
            if(projectileSpawner == null)
            {
                projectileSpawner = GetComponent<ObjectSpawner>();
            }
            projectileSpawner.horizontalDirection = value;
            setSpriteDirection();
        }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

    public void Start()
    {
		messenger = GetComponent<IMessenger>();
        if(projectileSpawner == null)
        {
            projectileSpawner = GetComponent<ObjectSpawner>();
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
	}

	void Update()
    {
		firingTimePassed += Time.deltaTime;
		if (firingTimePassed >= firingDelay)
        {
			firingTimePassed = 0;
			GameObject projectile = projectileSpawner.SpawnObject();
			ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement> ();
			projectileMovement.speed = projectileSpeed;
            projectileMovement.horizontalDirection = spawnDirection;
			messenger.Invoke (Message.PROJECTILE_FIRED, null);
		}
	}

    private void setSpriteDirection()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.flipX = (spawnDirection == HorizontalDirection.RIGHT);
    }
}
