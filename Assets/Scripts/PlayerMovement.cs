using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float jumpSpeed;
    public float shortHopJumpSpeed;
	public float moveSpeed;

	private Rigidbody2D rigidbodyObject;
	private BoxCollider2D boxCollider;
    private float colliderMargin = 0.05f;
	private float groundMargin = 0.1f;

	private Vector2 acceleration;
	private Direction movementDirection;
	public Direction MovementDirection
    {
		get { return this.movementDirection; }
		set
        {
			this.movementDirection = value;
			this.acceleration = new Vector2 ((float)value * this.moveSpeed, this.acceleration.y);
		}
	}

    void Awake()
    {
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
		acceleration = new Vector2 (0.0f, 0.0f);
    }

	public bool IsGrounded()
	{
		Vector2 leftOrigin = new Vector2 (transform.position.x - (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
		Vector2 rightOrigin = new Vector2 (transform.position.x + (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
		return Physics2D.Raycast(leftOrigin, Vector2.down, groundMargin) || Physics2D.Raycast(rightOrigin, Vector2.down, groundMargin);
	}

    //Full hop. If needGrounded is true, the player will only full hop if grounded
    public void FullHop(bool needGrounded)
	{
		if(rigidbodyObject != null && (!needGrounded || IsGrounded()))
		{
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
		}
	}

    //Short hop. If needGrounded is true, the player will only short hop if grounded
	public void ShortHop(bool needGrounded)
    {
        if (rigidbodyObject != null && (!needGrounded || IsGrounded()))
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, shortHopJumpSpeed);
        }
    }

	public void Update()
    {
        this.rigidbodyObject.velocity += this.acceleration * Time.deltaTime;
	}
}