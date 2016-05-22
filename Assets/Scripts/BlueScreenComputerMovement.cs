using UnityEngine;

public class BlueScreenComputerMovement : MonoBehaviour {

    public HorizontalDirection horizontalDirection;
    public float horizontalSpeed;
    public float movementTime; //How long the enemy moves before stopping
    public float stopTime; //How long the enemy stays stopped

    private float currentTimePassed; //How long has passed since the last change of movement state
    private bool moving; //Is the enemy moving
    private Rigidbody2D rigidbodyObject;
    private IMessenger messenger;

    void Start()
    {
        rigidbodyObject = GetComponent<Rigidbody2D>();
        messenger = GetComponent<IMessenger>();
        rigidbodyObject.velocity = new Vector2((float)horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
        moving = true;
    }

    void Update()
    {
        currentTimePassed += Time.deltaTime;
        if (moving)
        {
            rigidbodyObject.velocity = new Vector2((float)horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
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

    private void stopMovement()
    {
        rigidbodyObject.velocity = Vector2.zero;
        moving = false;
        currentTimePassed = 0;
        messenger.Invoke("StoppedMovement", null);
    }

    private void startMovement()
    {
        rigidbodyObject.velocity = new Vector2((float)horizontalDirection * horizontalSpeed, rigidbodyObject.velocity.y);
        moving = true;
        currentTimePassed = 0;
        messenger.Invoke("StartedMovement", null);
    }
}
