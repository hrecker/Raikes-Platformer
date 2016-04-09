using UnityEngine;

public class LeenKiatMovement : MonoBehaviour {

    public Direction horizontalDirection;
    public float normalHorizontalSpeed;
    public float squishedHorizontalSpeed;
    public float jumpSpeed;
    private Rigidbody2D rigidbodyObject;
    public bool squished;
    
    void Start ()
    {
        squished = false;
        rigidbodyObject = GetComponent<Rigidbody2D>();
        rigidbodyObject.velocity = new Vector2((float) horizontalDirection * normalHorizontalSpeed, rigidbodyObject.velocity.y);
	}
	
	void Update ()
    {
        if(!squished)
        {
            rigidbodyObject.velocity = new Vector2((float) horizontalDirection * normalHorizontalSpeed, rigidbodyObject.velocity.y);
        }
        else
        {
            rigidbodyObject.velocity = new Vector2((float) horizontalDirection * squishedHorizontalSpeed, rigidbodyObject.velocity.y);
        }
    }

    public void Squish()
    {
        squished = true;
    }
}
