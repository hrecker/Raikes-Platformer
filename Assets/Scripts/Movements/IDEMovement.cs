using UnityEngine;

public class IDEMovement : MonoBehaviour, IDirected
{
    public HorizontalDirection _horizontalDirection;
    public Turnbox turnbox;
    public float speed;
    public float jumpSpeed;
    private Rigidbody2D rigidbodyObject;

    public HorizontalDirection horizontalDirection
    {
        get { return _horizontalDirection; }
        set { _horizontalDirection = value; }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float)_horizontalDirection * speed, rigidbodyObject.velocity.y);
    }

    void Update()
    {
        rigidbodyObject.velocity = new Vector2((float)_horizontalDirection * speed, rigidbodyObject.velocity.y);
    }

    public void Jump()
    {
        rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
    }

    public void Turn()
    {
        _horizontalDirection = (HorizontalDirection)((float)_horizontalDirection * -1);
        SetActiveTurnbox(_horizontalDirection);
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