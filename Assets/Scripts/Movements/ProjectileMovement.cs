using UnityEngine;

public class ProjectileMovement: MonoBehaviour, IDirected
{
	public HorizontalDirection direction;
	public float speed;
	public float duration;
	private float timePassed;
	private IMessenger messenger;

    public HorizontalDirection horizontalDirection
    {
        get { return direction; }
        set { MoveInDirection(value); }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

    public void Start()
    {
		messenger = GetComponent<IMessenger> ();
	}

	public void MoveInDirection(HorizontalDirection direction)
    {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * (float)direction, 0.0f);
		this.direction = direction;
	}

	public void Update()
    {
		timePassed += Time.deltaTime;
		if (timePassed >= duration)
        {
			messenger.Invoke (Message.PROJECTILE_EXPIRED, null);
			Expire ();
		}
	}

	public void Expire()
    {
		Destroy (gameObject);
	}

}
