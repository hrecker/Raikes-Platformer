using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float jumpSpeed;
    public float shortHopJumpSpeed;
	public float moveSpeed;

	private Rigidbody2D rigidbodyObject;
	private BoxCollider2D boxCollider;
	private float groundMargin = 0.1f;

	private Vector2 acceleration;
	private Direction movementDirection;
	public Direction MovementDirection {
		get { return this.movementDirection; }
		set {
			this.movementDirection = value;
			this.acceleration = new Vector2 ((float)value * this.moveSpeed, this.acceleration.y);
		}
	}

	void Start()
	{
		rigidbodyObject = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		acceleration = new Vector2 (0.0f, 0.0f);
	}

	public bool IsGrounded()
	{
		Vector2 origin = new Vector2 (transform.position.x, transform.position.y - (boxCollider.bounds.size.y / 2.0f));
		return Physics2D.Raycast(origin, Vector2.down, groundMargin);
	}

	public void FullHop()
	{
		//this.canShortHop = true;
		if(rigidbodyObject != null && IsGrounded())
		{
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
		}
	}

	public void ShortHop() {
        if (rigidbodyObject != null && IsGrounded())
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, shortHopJumpSpeed);
        }
    }

	public void Update() {
		this.rigidbodyObject.velocity += this.acceleration * Time.deltaTime;
	}
}
