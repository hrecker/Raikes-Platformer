using UnityEngine;

public interface IMovement
{
	HorizontalDirection Direction { get; set; }
}

public class LaundryProjectileSpawner: MonoBehaviour, IMovement
{
    public float firingDelay;
    public float projectileSpeed;
    public HorizontalDirection spawnDirection;
	public HorizontalDirection Direction
    {
		get { return spawnDirection; }
		set { spawnDirection = value; }
	}

	private float firingTimePassed;
	private ObjectSpawner projectileSpawner;
	private IMessenger messenger;

	public void Start()
    {
		messenger = GetComponent<IMessenger>();
		projectileSpawner = GetComponent<ObjectSpawner> ();
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
			projectileMovement.MoveInDirection (spawnDirection);
			messenger.Invoke (Message.PROJECTILE_FIRED, null);
		}
	}
}
