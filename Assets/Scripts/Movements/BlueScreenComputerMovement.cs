using UnityEngine;

public class BlueScreenComputerMovement : MonoBehaviour, IDirected
{
    public HorizontalDirection _horizontalDirection;
    public Turnbox turnbox;
    public float horizontalSpeed;
    public float movementTime; //How long the enemy moves before stopping
    public float stopTime; //How long the enemy stays stopped
    public float jumpSpeed;

    private float currentTimePassed; //How long has passed since the last change of movement state
    private bool moving; //Is the enemy moving
    private Rigidbody2D rigidbodyObject;
    private IMessenger messenger;

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
        messenger = GetComponent<IMessenger>();
        rigidbodyObject.velocity = new Vector2((float)_horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
        moving = true;
    }

    void Update()
    {
        currentTimePassed += Time.deltaTime;
        if (moving)
        {
            rigidbodyObject.velocity = new Vector2((float)_horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
        }

        if(moving && currentTimePassed >= movementTime)
        {
            stopMovement();
        }

        if(!moving && currentTimePassed >= stopTime)
        {
            startMovement();
        }
    }

    public void Jump()
    {
        rigidbodyObject.velocity = new Vector2(rigidbodyObject.velocity.x, jumpSpeed);
    }

    private void stopMovement()
    {
        rigidbodyObject.velocity = Vector2.zero;
        moving = false;
        currentTimePassed = 0;
        messenger.Invoke(Message.STOPPED_MOVEMENT, null);
    }

    private void startMovement()
    {
        rigidbodyObject.velocity = new Vector2((float)_horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
        moving = true;
        currentTimePassed = 0;
        messenger.Invoke(Message.STARTED_MOVEMENT, null);
    }

    public void Turn()
    {
        if(moving)
        {
            _horizontalDirection = (HorizontalDirection)((float)_horizontalDirection * -1);
            SetActiveTurnbox(_horizontalDirection);
        }
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
