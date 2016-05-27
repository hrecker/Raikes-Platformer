using UnityEngine;

public class ShariCatMovement : MonoBehaviour
{
    public HorizontalDirection direction;
    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    private float currentTimePassed;
    private Rigidbody2D rigidbodyObject;
    private BoxCollider2D boxCollider;
    private float colliderMargin = 0.05f;
 	private float groundMargin = 0.1f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float)direction * speed, rigidbodyObject.velocity.y);
        if(direction == HorizontalDirection.RIGHT)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update()
    {
        currentTimePassed += Time.deltaTime;
        rigidbodyObject.velocity = new Vector2((float)direction * speed, rigidbodyObject.velocity.y);
        if(currentTimePassed >= jumpTime)
        {
            currentTimePassed = 0;
            if (IsGrounded())
            {
                rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
            }
        }
    }

    public bool IsGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        Vector2 leftOrigin = new Vector2(transform.position.x - (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        Vector2 rightOrigin = new Vector2(transform.position.x + (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        return Physics2D.Raycast(origin, Vector2.down, groundMargin) || Physics2D.Raycast(leftOrigin, Vector2.down, groundMargin) || Physics2D.Raycast(rightOrigin, Vector2.down, groundMargin);
    }
}
