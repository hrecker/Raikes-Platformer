using UnityEngine;

public class ShariCatMovement : MonoBehaviour, IDirected
{
    public HorizontalDirection direction;
    public Turnbox turnbox;
    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    private float currentTimePassed;
    private Rigidbody2D rigidbodyObject;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private float colliderMargin = 0.05f;
 	private float groundMargin = 0.1f;

    public HorizontalDirection horizontalDirection
    {
        get { return direction; }
        set { direction = value; }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float)direction * speed, rigidbodyObject.velocity.y);
        if(direction == HorizontalDirection.RIGHT)
        {
            spriteRenderer.flipX = true;
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
                Jump();
            }
        }
    }

    public void Jump()
    {
        rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
    }

    public bool IsGrounded()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        Vector2 leftOrigin = new Vector2(transform.position.x - (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        Vector2 rightOrigin = new Vector2(transform.position.x + (boxCollider.bounds.size.x / 2.0f), transform.position.y - (boxCollider.bounds.size.y / 2.0f) - colliderMargin);
        return Physics2D.Raycast(origin, Vector2.down, groundMargin) || Physics2D.Raycast(leftOrigin, Vector2.down, groundMargin) || Physics2D.Raycast(rightOrigin, Vector2.down, groundMargin);
    }

    public void Turn()
    {
        direction = (HorizontalDirection)((float)direction * -1);
        if (direction == HorizontalDirection.RIGHT)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        SetActiveTurnbox(direction);
    }

    public void SetActiveTurnbox(HorizontalDirection direction)
    {
        if (direction == HorizontalDirection.LEFT)
        {
            turnbox.DeactivateRightmostBox();
        }
        else if (direction == HorizontalDirection.RIGHT)
        {
            turnbox.DeactiveLeftmostBox();
        }
    }
}
