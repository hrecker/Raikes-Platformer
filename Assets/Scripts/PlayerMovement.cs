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
		Vector2 origin = new Vector2 (transform.position.x, transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
		return Physics2D.Raycast(origin, Vector2.down, groundMargin);
	}

	public void FullHop()
	{
		if (rigidbodyObject != null && IsGrounded())
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

	public void OnCollisionEnter2D(Collision2D collision) {
		//TODO Find a way to ACTUALLY identify different types of collisions.
		if (collision.gameObject.tag.Equals ("Untagged")) {
			LeenKiatMovement em = collision.gameObject.GetComponent<LeenKiatMovement>();
			if (this.landedOnObject(collision) && em != null) {
				em.Squish ();
				if (Input.GetKey (KeyCode.Space)) {
					this.FullHop ();
				} else {
					this.ShortHop ();
				}
			}
		}
	}

	public bool landedOnObject(Collision2D collision) {
		Vector2 collisionPoint = PlayerMovement.AverageContacts (collision.contacts);
		if (PlayerMovement.overlapIsHorizontal (collision.contacts)) {
			float yBuffer = 1.0f;
			float maxY = this.transform.position.y - (boxCollider.bounds.size.y / 2.0f) + yBuffer;
			float minY = collision.transform.position.y - collision.gameObject.GetComponent<BoxCollider2D> ().bounds.size.y / 2.0f - yBuffer;
			return minY <= collisionPoint.y && collisionPoint.y <= maxY;
		} else {
			return false;
		}
	}

	public static Vector2 AverageContacts(ContactPoint2D[] contacts) {
		Vector2 position = new Vector2();
		foreach (var c in contacts) {
			position += c.point;
		}
		return position / contacts.Length;
	}

	public static bool overlapIsHorizontal(ContactPoint2D[] contacts) {
		if (contacts.Length <= 1) {
			return true;
		}
		//Relies on there only being two contact points
		return contacts [0].point.y == contacts [1].point.y;
	}
}
