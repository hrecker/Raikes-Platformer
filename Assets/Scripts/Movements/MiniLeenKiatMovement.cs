using UnityEngine;

public class MiniLeenKiatMovement : MonoBehaviour, IDirected
{
    public HorizontalDirection _horizontalDirection;
    public float normalHorizontalSpeed;
    public float squishedHorizontalSpeed;
    public float jumpSpeed;
    public Turnbox turnbox;

    private Rigidbody2D rigidbodyObject;
    private bool squished;

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

    public bool Squished
    {
        get { return squished; }
    }
    
    void Start ()
    {
        squished = false;
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float) _horizontalDirection * normalHorizontalSpeed, rigidbodyObject.velocity.y);
        SetActiveTurnbox(_horizontalDirection);
	}
	
	void Update ()
    {
        if(!squished)
        {
            rigidbodyObject.velocity = new Vector2((float) _horizontalDirection * normalHorizontalSpeed, rigidbodyObject.velocity.y);
        }
        else
        {
            rigidbodyObject.velocity = new Vector2((float) _horizontalDirection * squishedHorizontalSpeed, rigidbodyObject.velocity.y);
        }
    }

    public void Jump()
    {
        rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
    }

    public void Squish()
    {
        squished = true;
    }

    public void Turn()
    {
        _horizontalDirection = (HorizontalDirection)((float)_horizontalDirection * -1);
        SetActiveTurnbox(_horizontalDirection);
    }

    public void SetActiveTurnbox(HorizontalDirection direction)
    {
        if(direction == HorizontalDirection.LEFT)
        {
            turnbox.DeactivateRightmostBox();
        }
        else if(direction == HorizontalDirection.RIGHT)
        {
            turnbox.DeactiveLeftmostBox();
        }
    }
}
