using UnityEngine;

public class BuzzwordLetterMovement : MonoBehaviour, IDirected
{
    private float speed;
    private HorizontalDirection _horizontalDirection;
    private float directionChangeTime;
    private Rigidbody2D rigidbodyObject;
    private bool high; //Is this letter in the high position or the low position
    private bool movingHorizontal; // Is this letter moving horizontally or vertically
    private float currentTimePassed;
    private bool active;

    public HorizontalDirection horizontalDirection
    {
        get { return _horizontalDirection; }
        set { _horizontalDirection = value; }
    }

    public VerticalDirection verticalDirection
    {
        get
        {
            if (movingHorizontal)
            {
                return high ? VerticalDirection.UP : VerticalDirection.DOWN;
            }
            else
            {
                return VerticalDirection.NONE;
            }
        }
        set { }
    }

    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = getVelocity();
    }

    void Update()
    {
        if (active)
        {
            currentTimePassed += Time.deltaTime;
            if (currentTimePassed >= directionChangeTime)
            {
                currentTimePassed = 0;
                rigidbodyObject.velocity = getVelocity();
            }
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void SetDirectionChangeTime(float changeTime)
    {
        directionChangeTime = changeTime;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    
    private Vector2 getVelocity()
    {
        VerticalDirection vert = high ? VerticalDirection.DOWN : VerticalDirection.UP;
        if (movingHorizontal)
        {
            high = !high;
            movingHorizontal = false;
            return new Vector2(0, speed * (float)vert);
        }
        else
        {
            movingHorizontal = true;
            return new Vector2((float)_horizontalDirection * speed, 0);
        }
    }
}
