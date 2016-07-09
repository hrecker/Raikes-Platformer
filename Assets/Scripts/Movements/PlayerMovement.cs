using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDirected
{
	public float jumpSpeed;
    public float shortHopJumpSpeed;
	public float moveSpeed;
    public float maxMoveSpeed;
    public float slowDownAcceleration;
    public float fastFallMultiplier;
    public float superFastFallMultiplier;
    public float superFastFallVerticalVelocity;
    public HorizontalDirection facingDirection;

    private IMessenger messenger;
	private Rigidbody2D rigidbodyObject;
	private BoxCollider2D boxCollider;
    private float colliderMargin = 0.10f;
	private float groundMargin = 0.1f;
    private PlayerState playerState;
    private float standardGravityScale;
	private Vector2 acceleration;
	private HorizontalDirection movementDirection;
	public bool touchingSideOfPlatform = false;
    
    public HorizontalDirection horizontalDirection
    {
        get { return movementDirection; }
        set
        {
            messenger.Invoke(Message.DIRECTION_CHANGE, new object[] { value });
            movementDirection = value;
            if(value != HorizontalDirection.NONE)
            {
                facingDirection = value;
            }
            acceleration = new Vector2((float)value * moveSpeed, acceleration.y);
        }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

    void Awake()
    {
        if(facingDirection == HorizontalDirection.NONE)
        {
            facingDirection = HorizontalDirection.RIGHT;
        }

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
		var hurtbox = this.GetComponentsInChildren<Hurtbox> () [0].BoxColliders[0];
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
            messenger.Invoke(Message.BOUNCE, null);
		}
	}

    //Short hop. If needGrounded is true, the player will only short hop if grounded
	public void ShortHop(bool needGrounded)
    {
        if (rigidbodyObject != null && (!needGrounded || IsGrounded()))
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, shortHopJumpSpeed);
            setPlayerState(PlayerState.JUMP);
            messenger.Invoke(Message.BOUNCE, null);
        }
    }

	public void TrampolinePlatformHop(float jumpSpeed, bool shortHop)
    {
		if (rigidbodyObject != null)
		{
			if (shortHop)
			{
				//We keep the ratio between short hop and full hop the
				//same as the ratio between short trampoline hop and full trampoline hop.
				jumpSpeed *= this.shortHopJumpSpeed / this.jumpSpeed;
			}
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
			setPlayerState(PlayerState.JUMP);
            messenger.Invoke(Message.BOUNCE, null);
        }
	}

	public void Update()
    {
		//When touching the side of a platform, the player's horizontal movement should stop.
		//touchingSideOfPlatform is reset at the end of each frame so the player doesn't get stuck.
		
		if(horizontalDirection != HorizontalDirection.NONE && !touchingSideOfPlatform)
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x + (acceleration.x * Time.deltaTime), rigidbodyObject.velocity.y);
            if(horizontalDirection == HorizontalDirection.RIGHT && rigidbodyObject.velocity.x > maxMoveSpeed)
            {
                rigidbodyObject.velocity = new Vector2(maxMoveSpeed, rigidbodyObject.velocity.y);
            }
            else if (horizontalDirection == HorizontalDirection.LEFT && rigidbodyObject.velocity.x < -maxMoveSpeed)
            {
                rigidbodyObject.velocity = new Vector2(-maxMoveSpeed, rigidbodyObject.velocity.y);
            }
        }
        else
        {
            if(rigidbodyObject.velocity.x > 0)
            {
                rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x - (slowDownAcceleration * Time.deltaTime), rigidbodyObject.velocity.y);
                if(rigidbodyObject.velocity.x < 0)
                {
                    rigidbodyObject.velocity = new Vector2(0, rigidbodyObject.velocity.y);
                }
            }
            else if(rigidbodyObject.velocity.x < 0)
            {
                rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x + (slowDownAcceleration * Time.deltaTime), rigidbodyObject.velocity.y);
                if (rigidbodyObject.velocity.x > 0)
                {
                    rigidbodyObject.velocity = new Vector2(0, rigidbodyObject.velocity.y);
                }
            }
        }

        if ((playerState == PlayerState.JUMP || playerState == PlayerState.STAND) && rigidbodyObject.velocity.y < 0)
        {
            setPlayerState(PlayerState.FALL);
        }
        else if(rigidbodyObject.velocity.y == 0)
        {
            setPlayerState(PlayerState.STAND);
        }
		touchingSideOfPlatform = false;
	}

    private void setPlayerState(PlayerState newState)
    {
        playerState = newState;
        messenger.Invoke(Message.STATE_CHANGE, new object[] { newState });
    }

    public void StartFastFall()
    {
		rigidbodyObject.gravityScale = standardGravityScale * fastFallMultiplier;
		IgnoreSoftPlatforms (true);
    }

    public void StartSuperFastFall()
    {
        // stop all horizontal movement
        rigidbodyObject.velocity = new Vector2(0, superFastFallVerticalVelocity);
        acceleration = new Vector2(0, 0);
        rigidbodyObject.gravityScale = standardGravityScale * fastFallMultiplier;
        IgnoreSoftPlatforms(true);
    }

    public void StopFastFall()
    {
		rigidbodyObject.gravityScale = standardGravityScale;
		IgnoreSoftPlatforms (false);
    }
		
	public void IgnoreSoftPlatforms(bool ignore)
    {
		int playerLayer = LayerMask.NameToLayer ("Player");
		int softPlatformLayer = LayerMask.NameToLayer ("Soft Platform");
		Physics2D.IgnoreLayerCollision (playerLayer, softPlatformLayer, ignore);
	}

}