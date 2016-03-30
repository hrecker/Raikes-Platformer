using UnityEngine;

public class Movement : MonoBehaviour {

    public float jumpSpeed;

    private Rigidbody2D rigidbodyObject;
    private BoxCollider2D boxCollider;
    private float distanceToGround;
    private float groundMargin = 0.1f;
    
    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            distanceToGround = boxCollider.bounds.extents.y;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distanceToGround + groundMargin);
    }

    public void Jump()
    {
        if(rigidbodyObject != null && isGrounded())
        {
            rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
        }
    }
}
