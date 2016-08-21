using UnityEngine;

public class HongfengMovement : MonoBehaviour
{
    public float moveSpeed;
    public float leftBound;
    public float rightBound;
    public float stopHeight;
    public float stopMargin;
    public Vector3 midPoint;
    
    private bool stopping;
    private VerticalDirection verticalDirection;
    private HorizontalDirection horizontalDirection;

    void Start()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        verticalDirection = VerticalDirection.UP;
        SetColliderEnabled(false);
    }

    public void StopMovement()
    {
        stopping = true;
        SetColliderEnabled(true);
    }

    // enable/disable the hurtbox
    public void SetColliderEnabled(bool enabled)
    {
        GetComponent<BoxCollider2D>().enabled = enabled;
    }

    void Update()
    {
        if(verticalDirection == VerticalDirection.DOWN)
        {
            transform.position += (Vector3.down * moveSpeed);
            if(transform.position.y <= stopHeight)
            {
                verticalDirection = VerticalDirection.NONE;
                stopping = false;
            }
        }
        else if (verticalDirection == VerticalDirection.UP)
        {
            transform.position += (Vector3.up * moveSpeed);
            if (transform.position.y >= midPoint.y)
            {
                verticalDirection = VerticalDirection.NONE;
                horizontalDirection = HorizontalDirection.LEFT;
            }
        }

        if (horizontalDirection == HorizontalDirection.LEFT)
        {
            transform.position += (Vector3.left * moveSpeed);
            if(transform.position.x <= leftBound)
            {
                horizontalDirection = HorizontalDirection.RIGHT;
            }
        }
        else if (horizontalDirection == HorizontalDirection.RIGHT)
        {
            transform.position += (Vector3.right * moveSpeed);
            if (transform.position.x >= rightBound)
            {
                horizontalDirection = HorizontalDirection.LEFT;
            }
        }

        if(stopping && (Mathf.Abs(transform.position.x - midPoint.x) <= stopMargin))
        {
            horizontalDirection = HorizontalDirection.NONE;
            verticalDirection = VerticalDirection.DOWN;
        }
    }
}
