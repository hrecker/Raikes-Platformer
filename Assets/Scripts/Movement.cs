using UnityEngine;

public class Movement : MonoBehaviour {

	public float jumpSpeed;
	public float moveSpeed;

	private Rigidbody2D rigidbodyObject;
	private BoxCollider2D boxCollider;
	private float distanceToGround;
	private float groundMargin = 0.1f;

	private Vector2 acceleration;
	private float horizontalFactor;
	public float HorizontalFactor {
		get { return this.horizontalFactor; }
		set {
			this.horizontalFactor = value;
			this.acceleration = new Vector2 (this.moveSpeed * this.horizontalFactor, this.acceleration.y);
		}
	}
	private bool canShortHop;
	private readonly float shortHopFactor = 0.6f;

	void Start()
	{
		rigidbodyObject = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		if (boxCollider != null)
		{
			distanceToGround = boxCollider.bounds.extents.y;
		}
		acceleration = new Vector2 (0.0f, 0.0f);
		this.canShortHop = true;
	}

	private bool isGrounded()
	{
		Vector2 origin = new Vector2 (transform.position.x, transform.position.y - boxCollider.bounds.size.y / 2.0f);
		return Physics2D.Raycast(origin, Vector2.down, distanceToGround + groundMargin);
	}

	public void Jump()
	{
		this.canShortHop = true;
		if(rigidbodyObject != null && isGrounded())
		{
			rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
		}
	}

	public void TryShortHop(float vertical) {
		if (!this.canShortHop || vertical > 0.1) {
			return;
		}
		this.canShortHop = false;
		rigidbodyObject.velocity = new Vector2 (rigidbodyObject.velocity.x, rigidbodyObject.velocity.y * this.shortHopFactor);
	}

	public void Update() {
		this.rigidbodyObject.velocity += this.acceleration * Time.deltaTime;
	}
}
