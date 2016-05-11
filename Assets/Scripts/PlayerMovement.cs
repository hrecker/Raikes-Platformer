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
		var hurtbox = this.GetComponentsInChildren<Hurtbox> () [0].HurtboxCollider;
		Vector2 bottomLeft = new Vector2 (transform.position.x - (boxCollider.bounds.size.x / 2.0f), hurtbox.bounds.center.y - hurtbox.bounds.extents.y - colliderMargin);
		return Physics2D.Raycast (bottomLeft, Vector2.right, boxCollider.bounds.size.x + groundMargin);
	}

	public void FullHop()
	{
		if(rigidbodyObject != null && IsGrounded())
		{
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
		}
	}

	public void ShortHop()
    {
        if (rigidbodyObject != null && IsGrounded())
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, shortHopJumpSpeed);
        }
    }

	public void Update()
    {
        this.rigidbodyObject.velocity += this.acceleration * Time.deltaTime;
	}
}