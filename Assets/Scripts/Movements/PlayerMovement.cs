using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float jumpSpeed;
    public float shortHopJumpSpeed;
	public float moveSpeed;
    public float fastFallMultiplier;

    private IMessenger messenger;
	private Rigidbody2D rigidbodyObject;
	private BoxCollider2D boxCollider;
    private float colliderMargin = 0.10f;
	private float groundMargin = 0.1f;
    private PlayerState playerState;
    private float standardGravityScale;

	private Vector2 acceleration;
	private HorizontalDirection movementDirection;
	public HorizontalDirection MovementDirection
    {
		get { return this.movementDirection; }
		set
        {
            messenger.Invoke("DirectionChange", new object[] { value });
			this.movementDirection = value;
			this.acceleration = new Vector2 ((float)value * this.moveSpeed, this.acceleration.y);
		}
	}

    void Awake()
    {
        messenger = GetComponent<IMessenger>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Make hitboxes, hurtboxes, and main collider ignore eachother
        Hitbox[] hitboxes = GetComponentsInChildren<Hitbox>();
        Hurtbox[] hurtboxes = GetComponentsInChildren<Hurtbox>();
        foreach (Hitbox hitbox in hitboxes)
        {
            Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(), boxCollider);
            foreach (Hurtbox hurtbox in hurtboxes)
            {
                Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(), hurtbox.GetComponent<Collider2D>());
                Physics2D.IgnoreCollision(hurtbox.GetComponent<Collider2D>(), boxCollider);
            }
        }
    }

	void Start()
	{
		rigidbodyObject = GetComponent<Rigidbody2D>();
        standardGravityScale = rigidbodyObject.gravityScale;
		acceleration = new Vector2 (0.0f, 0.0f);
    }

	public bool IsGrounded()
	{
		var hurtbox = this.GetComponentsInChildren<Hurtbox> () [0].HurtboxCollider;
		Vector2 bottomLeft = new Vector2 (transform.position.x - (boxCollider.bounds.size.x / 2.0f), hurtbox.bounds.center.y - hurtbox.bounds.extents.y - colliderMargin);
		return Physics2D.Raycast (bottomLeft, Vector2.right, boxCollider.bounds.size.x + groundMargin);
	}

    //Full hop. If needGrounded is true, the player will only full hop if grounded
    public void FullHop(bool needGrounded)
	{
		if(rigidbodyObject != null && (!needGrounded || IsGrounded()))
		{
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
            setPlayerState(PlayerState.JUMP);
		}
	}

    //Short hop. If needGrounded is true, the player will only short hop if grounded
	public void ShortHop(bool needGrounded)
    {
        if (rigidbodyObject != null && (!needGrounded || IsGrounded()))
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, shortHopJumpSpeed);
            setPlayerState(PlayerState.JUMP);
        }
    }

	public void Update()
    {
        this.rigidbodyObject.velocity += this.acceleration * Time.deltaTime;
        if(playerState == PlayerState.JUMP && rigidbodyObject.velocity.y < 0)
        {
            setPlayerState(PlayerState.FALL);
        }
        else if(rigidbodyObject.velocity.y == 0)
        {
            setPlayerState(PlayerState.STAND);
        }
	}

    private void setPlayerState(PlayerState newState)
    {
        playerState = newState;
        messenger.Invoke("StateChange", new object[] { newState });
    }

    public void StartFastFall()
    {
        rigidbodyObject.gravityScale = standardGravityScale * fastFallMultiplier;
    }

    public void StopFastFall()
    {
        rigidbodyObject.gravityScale = standardGravityScale;
    }
}